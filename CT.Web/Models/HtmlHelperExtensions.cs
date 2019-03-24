using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace CT.Web.Models
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString Span(this HtmlHelper Helper, string Name, string Content, object HtmlAttributes)
        {
            var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(HtmlAttributes);

            TagBuilder tag = new TagBuilder("Span");
            tag.MergeAttribute("name", TagBuilder.CreateSanitizedId(Name));
            tag.GenerateId(Name);
            tag.InnerHtml = Content;
            foreach (var i in htmlAttributes)
            {
                tag.MergeAttribute(i.Key, i.Value.ToString());
            }
            return MvcHtmlString.Create(tag.ToString());
        }

        public static MvcHtmlString Span(this HtmlHelper Helper, string Name, string Content)
        {
            TagBuilder tag = new TagBuilder("Span");
            tag.MergeAttribute("name", TagBuilder.CreateSanitizedId(Name));
            tag.GenerateId(Name);
            tag.InnerHtml = Content;
            return MvcHtmlString.Create(tag.ToString());
        }

        public static MvcHtmlString SpanFor<TModel, TProperty>(this HtmlHelper<TModel> Helper, Expression<Func<TModel, TProperty>> expression, string Content, object HtmlAttributes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metaData = ModelMetadata.FromLambdaExpression(expression, Helper.ViewData);
            return Span(Helper, name, metaData.Model as string, HtmlAttributes);
        }

        public static MvcHtmlString SpanFor<TModel, TProperty>(this HtmlHelper<TModel> Helper, Expression<Func<TModel, TProperty>> expression, string Content)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metaData = ModelMetadata.FromLambdaExpression(expression, Helper.ViewData);
            return Span(Helper, name, metaData.Model as string);
        }
    }
}