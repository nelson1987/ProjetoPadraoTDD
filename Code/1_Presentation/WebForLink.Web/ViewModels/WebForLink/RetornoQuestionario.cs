using System.Collections.Generic;

namespace WebForLink.Web.ViewModels
{
    public class RetornoQuestionario<T> where T : class
    {
        public bool HabilitaBotoesEdicao { get; set; }
        public List<T> QuestionarioDinamicoList { get; set; }
    }
}