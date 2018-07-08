
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;

namespace QPager
{
    public static class HtmlHelper
    {

        private static TagBuilder GenerateItem(string text, string href)
        {
            var li = new TagBuilder("li");
            var a = new TagBuilder("a");
            a.InnerHtml.Append(text);
            a.Attributes.Add("href", href);
            li.InnerHtml.AppendHtml(a);
            return li;
        }

        private static int PageCount()
        {
            return 10;
        }

        private static TagBuilder GetNextPage(string text, string href)
        {
            return GenerateItem(text, href);
        }

        private static bool HasNextPage(int pageIndex, int total)
        {
            if ((pageIndex * GetItemPerPage()) < total) return true;
            return false;
        }

        private static TagBuilder GetPrevPage(string text, string href)
        {
            return GenerateItem(text, href);
        }
        private static bool HasPrevPage(int pageIndex)
        {
            if (pageIndex > 1) return true;
            return false;
        }


        private static int GetItemPerPage()
        {
            return 10;
        }

        private static string TagBuilderToString(TagBuilder tagBuilder, TagRenderMode renderMode = TagRenderMode.EndTag)
        {
            var encoder = HtmlEncoder.Create(new TextEncoderSettings());
            var writer = new System.IO.StringWriter() as TextWriter;
            tagBuilder.WriteTo(writer, encoder);
            return writer.ToString();
        }

        private static List<TagBuilder> GetPageListItems(int pageIndex, int totalPages, Func<int, string> page, QPageOptions options)
        {
            var Items = new List<TagBuilder>();

            int perPage = GetItemPerPage();
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            int allPages = totalPages / perPage;

            int start = (pageIndex / perPage) * GetItemPerPage();

            start = start == totalPages ? start - perPage : start;

            int end = start + perPage;
            if (end > allPages)
                end = allPages;

            if (HasPrevPage(pageIndex))
                Items.Add(GetPrevPage(options.PrevPageTitle, page(pageIndex - 1)));
            if (end - start > GetItemPerPage()) end = start + GetItemPerPage();
            if (start == 0)
                start = 1;
            if ((totalPages % perPage)>0 && (allPages *perPage) <totalPages)
                end++;
            for (int i = start; i <= end; i++)
            {
                var li = GenerateItem(i.ToString(), page(i));
           
                if (i == pageIndex)
                    li.AddCssClass("active");
                Items.Add(li);
            }

            if (HasNextPage(pageIndex, totalPages))
                Items.Add(GetNextPage(options.NextPageTitle, page(pageIndex + 1)));

            return Items;

        }

        private static string Generate(int pageIndex, int totalPages, Func<int, string> page, QPageOptions options)
        {
            var Items = GetPageListItems(pageIndex, totalPages, page, options);
            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");
            var ItemsString = Items.Aggregate(
          new StringBuilder(),
          (sb, listItem) => sb.Append(TagBuilderToString(listItem)),
          sb => sb.ToString());

            ul.InnerHtml.AppendHtml(ItemsString);
            return TagBuilderToString(ul);
        }

        public static HtmlString PagedList(this IHtmlHelper helper, int pageIndex, int totalPages, Func<int, string> page, QPageOptions options)
        {
            return new HtmlString(Generate(pageIndex, totalPages, page, options));
        }

        public static HtmlString PagedList(this IHtmlHelper helper, int pageIndex, int totalPages, Func<int, string> page)
        {
            return PagedList(helper, pageIndex, totalPages, page, new QPageOptions());
        }
    }
}
