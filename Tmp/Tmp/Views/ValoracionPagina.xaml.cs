using Plugin.InputKit.Shared.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmp.Helpers;
using Tmp.Models;
using Tmp.Services;
using Tmp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tmp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValoracionPagina : ContentPage
    {

        NavigationService navigationService;
        ApiService apiService;
        DialogService dialogService;
        MainViewModel mainViewModel;
        public MyFullPlan myFullPlan;
        public List<MyPlan> myplans;
        int PaginaActual = 1;
        int PaginaFinal = 1;
        public ObservableCollection<QuestionClass> myQuestions;




        public ValoracionPagina()
        {
            InitializeComponent();

            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            mainViewModel = MainViewModel.GetInstance();


            LoadPlan();
        }



        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            Debug.WriteLine("Focus");
            Editor objeto = (Editor)sender;
            objeto.Focus();
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            ViewCell vs = (ViewCell)sender;
            var editor = vs.FindByName<Editor>("txRespuesta");
            editor?.Focus();
        }


        async Task LoadPlan()
        {
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {

                await dialogService.ShowMessage(
                    "Error", Lenguages.Literal("CheckConnection"));
                return;

            }
            else
            {



                var response = await apiService.GetMyFullPlans<MyFullPlan>(

                (String)Application.Current.Resources["AzureUrl"],
                "/api",
                    "/MyPlans/GetMyPlanQuestions" +
                    "?idCustomer=" + mainViewModel.Token.Customer.CustomerId +
                    "&language=ES&idplan=1&idmyplan=1&keyplan=" + MainViewModel.GetInstance().TypePlan


                );


                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage(
                        "Error",
                        response.Message);
                    return;
                }


                myFullPlan = (MyFullPlan)response.Result;

                NombrePlan.Text = myFullPlan.PlanHead.Name;

                //MyQuestions = new ObservableCollection<QuestionClass>(myFullPlan.PlanQuestions.FindAll(a => a.Page == PaginaActual));
                myQuestions = new ObservableCollection<QuestionClass>(myFullPlan.PlanQuestions);


                foreach (var pregunta in myQuestions)
                {
                    // Stacklayout GLOBAL para cada pregunta
                    StackLayout stack = new StackLayout { Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.FillAndExpand };
                    stack.ClassId = "SL_" + pregunta.Page + "__" + pregunta.IdQuestion;

                    PaginaFinal = pregunta.Page;

                    Label lbLabel = new Label();

                    if (pregunta.IdTypeQuestion.Equals(2)) // TITULO
                    {
                        lbLabel.Text = pregunta.QuestionDesc;
                        lbLabel.FontAttributes = FontAttributes.Bold;
                        lbLabel.FontSize = 22;
                        lbLabel.ClassId = pregunta.IdQuestion;

                    }
                    else
                    {
                        lbLabel.Text = pregunta.QuestionDesc;
                        lbLabel.FontAttributes = FontAttributes.Bold;
                    }

                    stack.Children.Add(lbLabel);

                    lista.Children.Add(stack);

                    if (pregunta.IdTypeQuestion.Equals(1)) // TEXTO
                    {
                        Editor lbEditor = new Editor();
                        lbEditor.Placeholder = pregunta.MfResponses[0].AnswerDesc;
                        lbEditor.ClassId = pregunta.MfResponses[0].IdAnswer;
                        lbEditor.HeightRequest = 80;
                        stack.Children.Add(lbEditor);
                    }

                    if (pregunta.IdTypeQuestion.Equals(6)) // PICKER LISTA COMBO
                    {
                        Picker picker = new Picker
                        {
                            Title = Lenguages.Literal("SelectOption"),
                            ClassId = pregunta.IdQuestion,
                        };

                        Dictionary<string, string> PickerItems = new Dictionary<string, string>();
                        foreach (var respuestas in pregunta.MfResponses)
                        {
                            PickerItems.Add(respuestas.AnswerDesc, respuestas.IdAnswer);

                        };

                        foreach (string valores in PickerItems.Keys)
                        {
                            picker.Items.Add(valores);
                        }
                        stack.Children.Add(picker);

                    }


                    if (pregunta.IdTypeQuestion.Equals(0)) // CHECKBOX
                    {

                        StackLayout sl = new StackLayout();
                        sl.ClassId = pregunta.IdQuestion;


                        foreach (var respuestas in pregunta.MfResponses)
                        {
                            CheckBox ck = new CheckBox();
                            ck.ClassId = respuestas.IdAnswer;
                            ck.Text = respuestas.AnswerDesc;
                            ck.TextFontSize = 14;
                            sl.Children.Add(ck);

                        }
                        stack.Children.Add(sl);

                    }

                    if (pregunta.IdTypeQuestion.Equals(4)) // radiobutton
                    {
                        RadioButtonGroupView rbGroup = new RadioButtonGroupView();
                        rbGroup.ClassId = pregunta.IdQuestion;

                        foreach (var respuestas in pregunta.MfResponses)
                        {
                            RadioButton rb = new RadioButton();
                            rb.ClassId = respuestas.IdAnswer;
                            rb.Text = respuestas.AnswerDesc;
                            rb.Value = respuestas.IdAnswer;
                            rb.TextFontSize = 14;
                            rbGroup.Children.Add(rb);

                        }
                        stack.Children.Add(rbGroup);

                    }



                }

                PaginaFinal = PaginaFinal + 1;
                PaginarPreguntas();

            }

        }



        /***************************** FUNCIONES CAMPOS **************************************/



        private void BtAnterior_Clicked(object sender, EventArgs e)
        {
            PaginaActual--;
            PaginarPreguntas();
        }

        private async void BtSiguiente_Clicked(object sender, EventArgs e)
        {
            PaginaActual++;

            if (PaginaActual == PaginaFinal) // Nos vamos a diagnostico
            {
                NavigationService navigationService = new NavigationService();
                await navigationService.NavigateDetail("GenerandoPagina");
            }
            else
            {
                PaginarPreguntas();
            }




        }

        private void PaginarPreguntas()
        {

            scroll.ScrollToAsync(0, 0, false);

            var st = lista.Children.Where(x => x.ClassId.ToString().Contains("SL_"));

            Debug.WriteLine("Total Elementos: " + st.Count());
            foreach (var t in st)
            {

                Debug.WriteLine("Elemento>>>>>>> " + t.ClassId);
                Debug.WriteLine("Elemento Type >>>>>>> " + t.GetType().ToString());

                if (t.ClassId.ToString().Contains("SL_" + PaginaActual + "__"))
                {
                    t.IsVisible = true;
                }
                else
                {
                    t.IsVisible = false;
                }


            }

            if (PaginaActual == 1)
            {
                btAnterior.IsVisible = false;
            }
            else
            {
                btAnterior.IsVisible = true;
            }
            if (PaginaActual == PaginaFinal)
            {
                btSiguiente.IsVisible = false;
            }
            else
            {
                btSiguiente.IsVisible = true;
            }

        }
    }
}