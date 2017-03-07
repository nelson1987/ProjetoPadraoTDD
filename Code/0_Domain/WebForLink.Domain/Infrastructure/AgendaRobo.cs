using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace WebForLink.Domain.Infrastructure
{
    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.wfmc.org/2009/XPDL2.2")]
    [XmlRoot(Namespace = "http://www.wfmc.org/2009/XPDL2.2", IsNullable = false)]
    public class AgendaRobo
    {
        private AgendaRoboContratante[] contratantesField;
        //public AgendaRobo Agenda { get; set; }        
        public DateTime DataCarga { get; set; }

        /// <remarks />
        [XmlArrayItem("Contratante", IsNullable = false)]
        public AgendaRoboContratante[] Contratantes
        {
            get { return contratantesField; }
            set { contratantesField = value; }
        }
    }

    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.wfmc.org/2009/XPDL2.2")]
    public class AgendaRoboContratante
    {
        private string[] horaField;
        private byte idField;
        private string nomeContratanteField;

        /// <remarks />
        [XmlElement("hora")]
        public string[] hora
        {
            get { return horaField; }
            set { horaField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public byte id
        {
            get { return idField; }
            set { idField = value; }
        }

        /// <remarks />
        [XmlAttribute]
        public string nomeContratante
        {
            get { return nomeContratanteField; }
            set { nomeContratanteField = value; }
        }
    }
}