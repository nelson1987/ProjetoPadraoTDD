using System;
using System.Xml.Serialization;

namespace WebForLink.Web.Infrastructure
{
    /// <remarks/>
    [SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.wfmc.org/2009/XPDL2.2")]
    [XmlRootAttribute(Namespace = "http://www.wfmc.org/2009/XPDL2.2", IsNullable = false)]
    public partial class AgendaRobo
    {
        //public AgendaRobo Agenda { get; set; }        
        public DateTime DataCarga { get; set; }   

        private AgendaRoboContratante[] contratantesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Contratante", IsNullable = false)]
        public AgendaRoboContratante[] Contratantes
        {
            get
            {
                return this.contratantesField;
            }
            set
            {
                this.contratantesField = value;
            }
        }

        
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.wfmc.org/2009/XPDL2.2")]
    public partial class AgendaRoboContratante
    {

        private string[] horaField;

        private byte idField;

        private string nomeContratanteField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("hora")]
        public string[] hora
        {
            get
            {
                return this.horaField;
            }
            set
            {
                this.horaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nomeContratante
        {
            get
            {
                return this.nomeContratanteField;
            }
            set
            {
                this.nomeContratanteField = value;
            }
        }
    }


}