using System.Web.Mvc;

namespace WebForLink.Web.ActionResults
{
    public class PdfResult<T> : ActionResult
    {
        private readonly T _modelToSerialize;

        public PdfResult(T modelToSerialize)
        {
            _modelToSerialize = modelToSerialize;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            //var result = new ControllerContext();
            //result.Content - _modelToSerialize.Serialize();
            //result.ContentType = "text/xml";
            //result.ExecutedResult(context);
        }
    }
}