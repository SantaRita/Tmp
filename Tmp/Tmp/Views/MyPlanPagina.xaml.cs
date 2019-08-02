using Newtonsoft.Json;
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
    public partial class MyPlanPagina : ContentPage
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




        public MyPlanPagina()
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

                MyFullPlan formulario = null;

                if (MainViewModel.GetInstance().PlanActual != null)
                {
                    formulario = JsonConvert.DeserializeObject<MyFullPlan>(MainViewModel.GetInstance().PlanActual.Formulario);
                    
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
                        lbLabel.ClassId = pregunta.IdQuestion;
                    }

                    stack.Children.Add(lbLabel);

                    lista.Children.Add(stack);

                    if (pregunta.IdTypeQuestion.Equals(1)) // TEXTO
                    {

                        Editor lbEditor = new Editor( );
                        Label lbEditorObligatorio = new Label();
                        lbEditor.Placeholder = pregunta.MfResponses[0].AnswerDesc;
                        lbEditor.ClassId = pregunta.MfResponses[0].IdAnswer;
                        lbEditor.HeightRequest = 80;
                        lbEditor.Behaviors.Add(new BehaviorEditor(lbEditorObligatorio));
                        stack.Children.Add(lbEditor);

                        // Campo editor obligatorio
                        lbEditorObligatorio.ClassId = "Req_" + pregunta.MfResponses[0].IdAnswer;
                        lbEditorObligatorio.Text = Lenguages.Literal("RequiredAnswer");
                        lbEditorObligatorio.HeightRequest = 40;
                        stack.Children.Add(lbEditorObligatorio);



                        // Recuperamos el valor del formulario si es consulta
                        if ( MainViewModel.GetInstance().PlanActual != null  )
                        {
                            List<QuestionClass> resultado = formulario.PlanQuestions.Where(a => a.IdQuestion == pregunta.IdQuestion).ToList();

                            if ( resultado.Count() > 0)
                            {
                                lbEditor.Text = resultado[0].MfResponses[0].Respuesta;
                            }
                        }
                    }

                    if (pregunta.IdTypeQuestion.Equals(6)) // PICKER LISTA COMBO
                    {
                        Picker picker = new Picker
                        {
                            Title = Lenguages.Literal("SelectOption"),
                            ClassId = pregunta.IdQuestion.Replace("_PR", "_RE")
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

                        Label lbPickerObligatorio = new Label();

                        picker.Behaviors.Add(new BehaviorPicker(lbPickerObligatorio));


                        // Campo editor obligatorio
                        lbPickerObligatorio.ClassId = "Req_" + pregunta.MfResponses[0].IdAnswer;
                        lbPickerObligatorio.Text = Lenguages.Literal("RequiredAnswer");
                        lbPickerObligatorio.HeightRequest = 40;
                        stack.Children.Add(lbPickerObligatorio);

                        // Recuperamos el valor del combo si es consulta
                        if (MainViewModel.GetInstance().PlanActual != null)
                        {
                            List<QuestionClass> resultado = formulario.PlanQuestions.Where(a => a.IdQuestion == pregunta.IdQuestion).ToList();

                            if (resultado.Count() > 0)
                            {
                                picker.SelectedIndex = Int32.Parse(resultado[0].MfResponses[0].Respuesta);
                            }
                        }

                    }


                    if (pregunta.IdTypeQuestion.Equals(0)) // CHECKBOX
                    {

                        //StackLayout sl = new StackLayout();
                        //sl.ClassId = pregunta.IdQuestion;


                        foreach (var respuestas in pregunta.MfResponses)
                        {
                            CheckBox ck = new CheckBox();
                            ck.ClassId = respuestas.IdAnswer;
                            ck.Text = respuestas.AnswerDesc;
                            ck.TextFontSize = 14;
                            stack.Children.Add(ck);

                            // Recuperamos el valor del radio button si es consulta
                            ck.IsChecked = false;
                            if (MainViewModel.GetInstance().PlanActual != null)
                            {
                                List<QuestionClass> resultado = formulario.PlanQuestions.Where(a => a.IdQuestion == pregunta.IdQuestion).ToList();

                                if (resultado.Count() > 0)
                                {
                                    // RECORREMOS LAS POSIBLES RESPUESTAS DE LAS CHECK
                                    foreach (var resCk in resultado[0].MfResponses )
                                    {

                                        if ( resCk.IdAnswer.Equals(ck.ClassId) && resCk.Respuesta.Equals("True"))
                                        {
                                            ck.IsChecked = true;
                                        }
                                            
                                    }
                                       
                                }
                            }

                        }
                        //

                    }

                    if (pregunta.IdTypeQuestion.Equals(4)) // radiobutton
                    {
                        RadioButtonGroupView rbGroup = new RadioButtonGroupView();
                        rbGroup.ClassId = pregunta.IdQuestion.Replace("_PR","_RE");

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

                        Label lbRadioButtonObligatorio = new Label();
                        rbGroup.Behaviors.Add(new BehaviorRadioButton(lbRadioButtonObligatorio));


                        // Campo editor obligatorio
                        lbRadioButtonObligatorio.ClassId = "Req_" + pregunta.MfResponses[0].IdAnswer;
                        lbRadioButtonObligatorio.Text = Lenguages.Literal("RequiredAnswer");
                        lbRadioButtonObligatorio.HeightRequest = 40;
                        stack.Children.Add(lbRadioButtonObligatorio);


                        // Recuperamos el valor del radio button si es consulta
                        if (MainViewModel.GetInstance().PlanActual != null)
                        {
                            List<QuestionClass> resultado = formulario.PlanQuestions.Where(a => a.IdQuestion == pregunta.IdQuestion).ToList();

                            if (resultado.Count() > 0)
                            {
                                rbGroup.SelectedIndex = Int32.Parse(resultado[0].MfResponses[0].Respuesta);
                            }
                        }

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
                await navigationService.NavigateDetail("DiagnosticoPagina");
            }
            else {
                PaginarPreguntas();
            }

            // PROCESO PARA GUARDAR EL CUESTIONARIO, DE MOMENTO ES TEMPORAL, LO COMENTADO ARRIBA LO TENEMOS QUE DESCOMENTAR

            MyFullPlan plan = new MyFullPlan();
            MyPlan miPlan = new MyPlan();
            plan.PlanQuestions = new List<QuestionClass>();


            if ( MainViewModel.GetInstance().PlanActual is null)
            {
                miPlan = new MyPlan { Name = "TEMPORAL", CustomerId = MainViewModel.GetInstance().Token.Customer.CustomerId };
                plan.PlanHead = miPlan;
            }
            else {

                miPlan = MainViewModel.GetInstance().PlanActual;
                plan.PlanHead = miPlan;
            }


            foreach (View item in ((StackLayout)lista).Children)
            {
          
                //Debug.WriteLine("Item: " + item.ClassId.ToString());

                QuestionClass q = null;
                List<ResponseClass> lstRespuestas = new List<ResponseClass>();
                ResponseClass r;


                foreach (var pregunta in ((StackLayout)item).Children)
                {
                    //Debug.WriteLine("    SubItem: " + pregunta.ClassId.ToString() + " Tipo: " + pregunta.GetType().ToString());

                    if ( pregunta.ClassId.ToString().Contains("_PR") ) // Añadimos la pregunta
                    {
                        lstRespuestas.Clear();
                        q = new QuestionClass() { IdQuestion = pregunta.ClassId.ToString() };
                    }


                    if (pregunta.ClassId.ToString().Contains("_RE")) // Añadimos la respuesta 
                    {
                        
                        if (pregunta.GetType() == typeof(Editor))  {

                            r = new ResponseClass
                            {
                                IdAnswer = pregunta.ClassId.ToString(),
                                Respuesta = ((Editor)pregunta).Text,
                                TypeTexto = true
                                
                            };
                        }
                        else if (pregunta.GetType() == typeof(Picker))
                        {

                            r = new ResponseClass
                            {
                                IdAnswer = pregunta.ClassId.ToString(),
                                Respuesta = ((Picker)pregunta).SelectedIndex.ToString(),
                                TypeCombo = true
                            };
                        }
                        else if (pregunta.GetType() == typeof(RadioButtonGroupView))
                        {

                            r = new ResponseClass
                            {
                                IdAnswer = pregunta.ClassId.ToString(),
                                Respuesta = ((RadioButtonGroupView)pregunta).SelectedIndex.ToString(),
                                TypeRadio = true
                            };
                        }
                        else if (pregunta.GetType() == typeof(CheckBox))
                        {

                            r = new ResponseClass
                            {
                                IdAnswer = pregunta.ClassId.ToString(),
                                Respuesta = ((CheckBox)pregunta).IsChecked.ToString(),
                                TypeCheck = true
                            };
                        }
                        else {
                            r = new ResponseClass { IdAnswer = pregunta.ClassId.ToString() };
                        }
                        lstRespuestas.Add(r);

                        // Si es el último item del layout entonces ya podemos añadir la clase

                        //Debug.WriteLine("cuantos: " + (((StackLayout)item).Children.Count()));
                        if (((StackLayout)item).Children.Last().Equals(pregunta))
                        {
                            q.MfResponses = lstRespuestas;
                            plan.PlanQuestions.Add(q);
                        }

                    }

                }
            }

            


            var json = JsonConvert.SerializeObject(plan);

            miPlan.KeyPlan = MainViewModel.GetInstance().TypePlan;
            miPlan.Formulario = json;

            //Debug.WriteLine("Json____________________________________");
            //Debug.WriteLine(json.ToString());

            // GUARDAMOS AL PLAN PARA ESO LLAMAMOS AL WS.

            Response response;
            // Actualizamosregistro
            if (miPlan.IdMyPlan > 0)
            {
                response = await apiService.PutMyplan<MyPlan>(
                   (String)Application.Current.Resources["AzureUrl"],
                   "/api",
                       "/MyPlans/" + miPlan.IdMyPlan,
                       miPlan);
            }
            else
            {
                response = await apiService.PostMyplan<MyPlan>(
                   (String)Application.Current.Resources["AzureUrl"],
                   "/api",
                       "/MyPlans/PostMyplan",
                       miPlan);
            }


            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage(
                    "Error",
                    response.Message);
                return;
            }
            else
            {
                // Actualizamos el valor global del plan actual que estamos usando
                MainViewModel.GetInstance().PlanActual = miPlan;
            }




        }

        private void PaginarPreguntas()
        {

            scroll.ScrollToAsync(0, 0, false);

            var st = lista.Children.Where(x => x.ClassId.ToString().Contains("SL_"));

            //Debug.WriteLine("Total Elementos: " + st.Count());
            foreach (var t in st)
            {

                //Debug.WriteLine("Elemento>>>>>>> " + t.ClassId);
                //Debug.WriteLine("Elemento Type >>>>>>> " + t.GetType().ToString());

                if (t.ClassId.ToString().Contains("SL_" + PaginaActual + "__"))
                {
                    t.IsVisible = true;
                }
                else
                {
                    t.IsVisible = false;
                }


            }

            if ( PaginaActual == 1)
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

        private void Ayuda_Clicked(object sender, EventArgs e)
        {
            if (stAyuda.IsVisible) stAyuda.IsVisible = false;
            else stAyuda.IsVisible = true;
        }
    }
}