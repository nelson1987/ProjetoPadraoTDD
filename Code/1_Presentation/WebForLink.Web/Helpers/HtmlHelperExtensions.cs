using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace System.Web.Mvc.Html
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString Acessar(this HtmlHelper helper, string value = "Acessar")
        {
            return
                new MvcHtmlString("<button class=\"btn btn-warning btn-sm navbar-btn\" id=\"btnAcesso\">" + value +
                                  "</button>");
        }

        public static MvcHtmlString Aguarde(this HtmlHelper helper, string value = "Aguarde...")
        {
            return
                new MvcHtmlString("<div class=\"aguarde\"><div><img src=\"../Content/images/loading.gif\"/> " + value +
                                  "</div></div>");
        }

        public static MvcHtmlString BoxLoading(this HtmlHelper helper, string value = "Aguarde...")
        {
            return
                new MvcHtmlString(
                    "<div class=\"overlay hidden\"><div class=\"aguarde-img\"><img src=\"../Content/images/loading.gif\" /> " +
                    value + " </div></div> ");
        }

        public static MvcHtmlString Pesquisar(this HtmlHelper helper, string value = "Consultar")
        {
            return
                new MvcHtmlString(
                    "<button type=\"button\" class=\"btn btn-primary\" id=\"btnPesquisa\"><i class=\"fa fa-search\"></i> " +
                    value + "</button>");
        }

        public static MvcHtmlString Limpar(this HtmlHelper helper, string[] formIds, string value = "Limpar")
        {
            var dadosClique = new StringBuilder();

            if (formIds.Length == 0 || formIds == null)
                dadosClique.Append("$('#Codigo').val('');");
            else
                foreach (var item in formIds)
                {
                    dadosClique.Append(string.Format("$('#{0}').val('');", item));
                }

            return
                new MvcHtmlString("<button type=\"button\" class=\"btn btn-default\" onclick=\"" + dadosClique +
                                  "\"><i class=\"fa fa-eraser\"></i> " + value + "</button>");
        }

        public static MvcHtmlString Editar(this HtmlHelper helper, string value = "Editar", string link = "/Home")
        {
            return
                new MvcHtmlString("<button class=\"btn btn-primary btn-xs\" onclick=\"location.href='" + link +
                                  "';\"><i class=\"fa fa-pencil-square-o\"></i>" + value + "</button>");
        }

        public static MvcHtmlString Excluir(this HtmlHelper helper, string value = "Excluir", string link = "/Home")
        {
            return
                new MvcHtmlString("<button class=\"btn btn-primary btn-xs\" onclick=\"location.href='" + link +
                                  "';\"><i class=\"fa fa-trash\"></i>" + value + "</button>");
        }

        public static MvcHtmlString Alerta(this HtmlHelper helper, string tipo = "FichaAlertaSucessoTop")
        {
            return
                new MvcHtmlString(
                    string.Format(
                    @"<div class='row'>
                                    <div id='{0}' 
                    class='alert alert-success' 
                    role='alert' 
                    style='display: none; font-size: 14px; font-weight: bold; text-align: center;' 
                    onclick='$(this).fadeOut('fast')'></div>
                    </div>",tipo));
        }
        
        public static MvcHtmlString BreadCrumb(this HtmlHelper helper, string paginaPai, string nomePagina)
        {
            if (string.IsNullOrEmpty(paginaPai))
                paginaPai = "Home";
            var nome =
                string.Format(
                    @"<section class='content-header'>
                        <h1>{0}</h1>
                        <ol class='breadcrumb'>
                            <li><i class='fa fa-archive'></i> {1}</li>
                            <li class='active'>{0}</li>
                        </ol>
                    </section>",
                    nomePagina, paginaPai);
            return new MvcHtmlString(nome);
        }

        public static MvcHtmlString LabelForRequired<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            var html1 = html.LabelFor(expression);
            var asterisco = "&nbsp;<span class=\"text-danger\" style=\"font-weight:bold;\">*</span><br />";
            return new MvcHtmlString(string.Format("{0}{1}", html1, asterisco));
        }

        public static MvcHtmlString LabelForRequired<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            var html1 = html.LabelFor(expression, htmlAttributes);
            var asterisco = "&nbsp;<span class=\"text-danger\" style=\"font-weight:bold;\">*</span><br />";
            return new MvcHtmlString(string.Format("{0}{1}", html1, asterisco));
        }

        public static MvcHtmlString LabelForRequired<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var html1 = html.LabelFor(expression, htmlAttributes);
            var asterisco = "&nbsp;<span class=\"text-danger\" style=\"font-weight:bold;\">*</span><br />";
            return new MvcHtmlString(string.Format("{0}{1}", html1, asterisco));
        }

        public static MvcHtmlString LabelForRequired<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression, string labelText)
        {
            var html1 = html.LabelFor(expression, labelText);
            var asterisco = "&nbsp;<span class=\"text-danger\" style=\"font-weight:bold;\">*</span><br />";
            return new MvcHtmlString(string.Format("{0}{1}", html1, asterisco));
        }

        public static MvcHtmlString LabelForRequired<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression, string labelText, object htmlAttributes)
        {
            var html1 = html.LabelFor(expression, labelText, htmlAttributes);
            var asterisco = "&nbsp;<span class=\"text-danger\" style=\"font-weight:bold;\">*</span><br />";
            return new MvcHtmlString(string.Format("{0}{1}", html1, asterisco));
        }

        public static MvcHtmlString LabelForRequired<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression, string labelText, IDictionary<string, object> htmlAttributes)
        {
            var html1 = html.LabelFor(expression, labelText, htmlAttributes);
            var asterisco = "&nbsp;<span class=\"text-danger\" style=\"font-weight:bold;\">*</span><br />";
            return new MvcHtmlString(string.Format("{0}{1}", html1, asterisco));
        }
    }

    public static class UrlHelperExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="actionName"></param>
        /// <param name="routeValues"></param>
        /// <param name="keyValue">Ex.:string.Format("id={0}", id)</param>
        /// <returns></returns>
        //public static string LinkCriptografado(this UrlHelper helper, string actionName, string keyValue = "id=0")
        //{
        //    var Cripto = new EncryptDecryptQueryString();
        //    return helper.Action(actionName, new
        //    {
        //        chaveurl = Cripto.Criptografar(keyValue, "r10X310y")
        //    });
        //}
    }
}