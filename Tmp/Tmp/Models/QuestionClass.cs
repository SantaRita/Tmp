using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmp.Models
{
    public class QuestionClass : INotifyPropertyChanged
    {

        public string IdQuestion { get; set; }
        public int Page { get; set; }
        public int? IdTypeQuestion { get; set; }
        public string IdLanguaje { get; set; }
        public string QuestionDesc { get; set; }
        public string Color { get; set; }



        //public List<ResponseClass> MfResponses = new List<ResponseClass>();

        private ResponseClass mfresponses;
        public List<ResponseClass> MfResponses
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class ResponseClass : INotifyPropertyChanged
    {
        private string _idanswer;
        public string IdAnswer
        {
            get
            {
                return _idanswer;
            }
            set
            {
                if (_idanswer != value)
                {
                    _idanswer = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IdAnswer)));
                }
            }
        }


        private string _answerdesc;
        public string AnswerDesc
        {
            get
            {
                return _answerdesc;
            }
            set
            {
                if (_answerdesc != value)
                {
                    _answerdesc = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(AnswerDesc)));
                }
            }
        }

        private bool _typetexto;
        public bool TypeTexto
        {
            get
            {
                return _typetexto;
            }
            set
            {
                if (_typetexto != value)
                {
                    _typetexto = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(TypeTexto)));
                }
            }
        }

        private bool _typeswitch;
        public bool TypeSwitch
        {
            get
            {
                return _typeswitch;
            }
            set
            {
                if (_typeswitch != value)
                {
                    _typeswitch = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(TypeSwitch)));
                }
            }
        }

        private bool _typeradio;
        public bool TypeRadio
        {
            get
            {
                return _typeradio;
            }
            set
            {
                if (_typeradio != value)
                {
                    _typeradio = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(TypeRadio)));
                }
            }
        }

        private bool _typecheck;
        public bool TypeCheck
        {
            get
            {
                return _typecheck;
            }
            set
            {
                if (_typecheck != value)
                {
                    _typecheck = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(TypeCheck)));
                }
            }
        }

        private bool _typecombo;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool TypeCombo
        {
            get
            {
                return _typecombo;
            }
            set
            {
                if (_typecombo != value)
                {
                    _typecombo = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(TypeCombo)));
                }
            }
        }


    }
}
