#pragma checksum "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "23e8300b1e4ad8114d957017715c26b56116bafc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\_ViewImports.cshtml"
using MajorProjectFrontEnd;

#line default
#line hidden
#line 2 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\_ViewImports.cshtml"
using MajorProjectFrontEnd.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"23e8300b1e4ad8114d957017715c26b56116bafc", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dcadd21c59cca63fbca2be6605da4e5a96c6b633", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<QuestionDataModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("target"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SurveyResponseAsync", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(32, 11, true);
            WriteLiteral("<article>\r\n");
            EndContext();
            BeginContext(43, 3047, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5972c00dde4742a4893ec37775fd7ffd", async() => {
                BeginContext(108, 48, true);
                WriteLiteral("\r\n\t<input name=\"numberOfQuestions\" type=\"hidden\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 156, "\"", 176, 1);
#line 4 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 164, Model.Count, 164, 12, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(177, 40, true);
                WriteLiteral(">\r\n\t<input name=\"surveyID\" type=\"hidden\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 217, "\"", 243, 1);
#line 5 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 225, Model[0].SurveyID, 225, 18, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(244, 3, true);
                WriteLiteral(">\r\n");
                EndContext();
#line 6 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
 foreach (var question in Model)
{

#line default
#line hidden
                BeginContext(284, 65, true);
                WriteLiteral("\t<div class=\"question\">\r\n\t\t<div class=\"survey-question\">Question ");
                EndContext();
                BeginContext(351, 27, false);
#line 9 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
                                          Write(Model.IndexOf(question) + 1);

#line default
#line hidden
                EndContext();
                BeginContext(379, 2, true);
                WriteLiteral(": ");
                EndContext();
                BeginContext(382, 17, false);
#line 9 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
                                                                         Write(question.Question);

#line default
#line hidden
                EndContext();
                BeginContext(399, 8, true);
                WriteLiteral("</div>\r\n");
                EndContext();
                BeginContext(413, 5, true);
                WriteLiteral("\t\t\t<p");
                EndContext();
                BeginWriteAttribute("class", " class=\"", 418, "\"", 463, 2);
                WriteAttributeValue("", 426, "errorMessage", 426, 12, true);
#line 11 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue(" ", 438, question.QuestionNumber, 439, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(464, 7, true);
                WriteLiteral("></p>\r\n");
                EndContext();
#line 12 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"

			var options = question.Options.Split(",");


#line default
#line hidden
                BeginContext(522, 9, true);
                WriteLiteral("\t\t\t<input");
                EndContext();
                BeginWriteAttribute("name", " name=\"", 531, "\"", 579, 2);
                WriteAttributeValue("", 538, "question_options_", 538, 17, true);
#line 15 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 555, question.QuestionNumber, 555, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(580, 14, true);
                WriteLiteral(" type=\"hidden\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 594, "\"", 619, 1);
#line 15 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 602, question.Options, 602, 17, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(620, 12, true);
                WriteLiteral(">\r\n\t\t\t<input");
                EndContext();
                BeginWriteAttribute("name", " name=\"", 632, "\"", 678, 2);
                WriteAttributeValue("", 639, "question_title_", 639, 15, true);
#line 16 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 654, question.QuestionNumber, 654, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(679, 14, true);
                WriteLiteral(" type=\"hidden\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 693, "\"", 719, 1);
#line 16 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 701, question.Question, 701, 18, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(720, 3, true);
                WriteLiteral(">\r\n");
                EndContext();
#line 17 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"

			if (question.Type == "MQ")
			{

#line default
#line hidden
                BeginContext(762, 10, true);
                WriteLiteral("\t\t\t\t<input");
                EndContext();
                BeginWriteAttribute("name", " name=\"", 772, "\"", 817, 2);
                WriteAttributeValue("", 779, "question_type_", 779, 14, true);
#line 20 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 793, question.QuestionNumber, 793, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(818, 28, true);
                WriteLiteral(" type=\"hidden\" value=\"MQ\">\r\n");
                EndContext();
#line 21 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"

				for (var i = 0; i < options.Length; ++i)
				{
					/*
						if (i == 0)
						{
							<label class="radio">@options[i]<input type="radio" name="@question.QuestionNumber" value="@options[i]" checked required></label>
						}
						else
						{
					*/


#line default
#line hidden
                BeginContext(1117, 26, true);
                WriteLiteral("\t\t\t\t\t<label class=\"radio\">");
                EndContext();
                BeginContext(1144, 10, false);
#line 33 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
                                    Write(options[i]);

#line default
#line hidden
                EndContext();
                BeginContext(1154, 19, true);
                WriteLiteral("<input type=\"radio\"");
                EndContext();
                BeginWriteAttribute("name", " name=\"", 1173, "\"", 1204, 1);
#line 33 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 1180, question.QuestionNumber, 1180, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginWriteAttribute("value", " value=\"", 1205, "\"", 1224, 1);
#line 33 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 1213, options[i], 1213, 11, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1225, 20, true);
                WriteLiteral(" required></label>\r\n");
                EndContext();
#line 34 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"

				}
			}
			else if (question.Type == "RANGE")
			{

#line default
#line hidden
                BeginContext(1305, 10, true);
                WriteLiteral("\t\t\t\t<input");
                EndContext();
                BeginWriteAttribute("name", " name=\"", 1315, "\"", 1360, 2);
                WriteAttributeValue("", 1322, "question_type_", 1322, 14, true);
#line 39 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 1336, question.QuestionNumber, 1336, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1361, 42, true);
                WriteLiteral(" type=\"hidden\" value=\"RANGE\">\r\n\t\t\t\t<p>1 = ");
                EndContext();
                BeginContext(1404, 10, false);
#line 40 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
                  Write(options[0]);

#line default
#line hidden
                EndContext();
                BeginContext(1414, 13, true);
                WriteLiteral("</p>\r\n\t\t\t\t<p>");
                EndContext();
                BeginContext(1428, 10, false);
#line 41 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
              Write(options[2]);

#line default
#line hidden
                EndContext();
                BeginContext(1438, 3, true);
                WriteLiteral(" = ");
                EndContext();
                BeginContext(1442, 10, false);
#line 41 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
                            Write(options[1]);

#line default
#line hidden
                EndContext();
                BeginContext(1452, 17, true);
                WriteLiteral("</p>\r\n\t\t\t\t<select");
                EndContext();
                BeginWriteAttribute("name", " name=\"", 1469, "\"", 1500, 1);
#line 42 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 1476, question.QuestionNumber, 1476, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1501, 3, true);
                WriteLiteral(">\r\n");
                EndContext();
#line 43 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
                     for (var i = 1; i <= Int32.Parse(options[2]); ++i)
					{

#line default
#line hidden
                BeginContext(1570, 6, true);
                WriteLiteral("\t\t\t\t\t\t");
                EndContext();
                BeginContext(1576, 30, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7d651afde58b42ca94b87cbdb739ebc3", async() => {
                    BeginContext(1596, 1, false);
#line 45 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
                                      Write(i);

#line default
#line hidden
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#line 45 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
                           WriteLiteral(i);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1606, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 46 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
					}

#line default
#line hidden
                BeginContext(1616, 15, true);
                WriteLiteral("\t\t\t\t</select>\r\n");
                EndContext();
#line 48 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"

			}
			else if (question.Type == "NI")
			{

#line default
#line hidden
                BeginContext(1681, 10, true);
                WriteLiteral("\t\t\t\t<input");
                EndContext();
                BeginWriteAttribute("name", " name=\"", 1691, "\"", 1736, 2);
                WriteAttributeValue("", 1698, "question_type_", 1698, 14, true);
#line 52 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 1712, question.QuestionNumber, 1712, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1737, 73, true);
                WriteLiteral(" type=\"hidden\" value=\"NI\">\r\n\t\t\t\t<input class=\"text\" type=\"number\" min=\"0\"");
                EndContext();
                BeginWriteAttribute("name", " name=\"", 1810, "\"", 1841, 1);
#line 53 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 1817, question.QuestionNumber, 1817, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1842, 3, true);
                WriteLiteral(">\r\n");
                EndContext();
#line 54 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
			}
			else if (question.Type == "COMMENT")
			{

#line default
#line hidden
                BeginContext(1898, 10, true);
                WriteLiteral("\t\t\t\t<input");
                EndContext();
                BeginWriteAttribute("name", " name=\"", 1908, "\"", 1953, 2);
                WriteAttributeValue("", 1915, "question_type_", 1915, 14, true);
#line 57 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 1929, question.QuestionNumber, 1929, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1954, 46, true);
                WriteLiteral(" type=\"hidden\" value=\"COMMENT\">\r\n\t\t\t\t<textarea");
                EndContext();
                BeginWriteAttribute("name", " name=\"", 2000, "\"", 2031, 1);
#line 58 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 2007, question.QuestionNumber, 2007, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2032, 14, true);
                WriteLiteral("></textarea>\r\n");
                EndContext();
#line 59 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"


			}
			else if (question.Type == "TEXT")
			{


#line default
#line hidden
                BeginContext(2102, 10, true);
                WriteLiteral("\t\t\t\t<input");
                EndContext();
                BeginWriteAttribute("name", " name=\"", 2112, "\"", 2157, 2);
                WriteAttributeValue("", 2119, "question_type_", 2119, 14, true);
#line 65 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 2133, question.QuestionNumber, 2133, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2158, 53, true);
                WriteLiteral(" type=\"hidden\" value=\"TEXT\">\r\n\t\t\t\t<input class=\"text\"");
                EndContext();
                BeginWriteAttribute("name", " name=\"", 2211, "\"", 2242, 1);
#line 66 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 2218, question.QuestionNumber, 2218, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2243, 3, true);
                WriteLiteral(">\r\n");
                EndContext();
#line 67 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
			}
			else if (question.Type == "RANK")
			{


#line default
#line hidden
                BeginContext(2298, 10, true);
                WriteLiteral("\t\t\t\t<input");
                EndContext();
                BeginWriteAttribute("name", " name=\"", 2308, "\"", 2353, 2);
                WriteAttributeValue("", 2315, "question_type_", 2315, 14, true);
#line 71 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 2329, question.QuestionNumber, 2329, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2354, 30, true);
                WriteLiteral(" type=\"hidden\" value=\"RANK\">\r\n");
                EndContext();
#line 72 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"

				var numberOfRanks = options.Count();

				foreach (var option in options)
				{

#line default
#line hidden
                BeginContext(2474, 16, true);
                WriteLiteral("\t\t\t\t\t<span>Rank ");
                EndContext();
                BeginContext(2492, 36, false);
#line 77 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
                           Write(options.ToList().IndexOf(option) + 1);

#line default
#line hidden
                EndContext();
                BeginContext(2529, 35, true);
                WriteLiteral(":</span>\r\n\t\t\t\t\t<select class=\"rank\"");
                EndContext();
                BeginWriteAttribute("name", " name=\"", 2564, "\"", 2595, 1);
#line 78 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
WriteAttributeValue("", 2571, question.QuestionNumber, 2571, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2596, 3, true);
                WriteLiteral(">\r\n");
                EndContext();
#line 79 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
                         for (var i = 0; i < options.Count(); ++i)
						{
							if (i == options.ToList().IndexOf(option))
							{

#line default
#line hidden
                BeginContext(2719, 8, true);
                WriteLiteral("\t\t\t\t\t\t\t\t");
                EndContext();
                BeginContext(2727, 90, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a2ffd2afc8c0472c8b50ed178fec26da", async() => {
                    BeginContext(2798, 10, false);
#line 83 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
                                                                                                 Write(options[i]);

#line default
#line hidden
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#line 83 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
                                   WriteLiteral(options[i]);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 83 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
AddHtmlAttributeValue("", 2762, question.QuestionNumber, 2762, 24, false);

#line default
#line hidden
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2817, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 84 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
							}
							else
							{

#line default
#line hidden
                BeginContext(2852, 8, true);
                WriteLiteral("\t\t\t\t\t\t\t\t");
                EndContext();
                BeginContext(2860, 81, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "901daef7671943c590df475bf0e40dd0", async() => {
                    BeginContext(2922, 10, false);
#line 87 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
                                                                                        Write(options[i]);

#line default
#line hidden
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#line 87 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
                                   WriteLiteral(options[i]);

#line default
#line hidden
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 87 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
AddHtmlAttributeValue("", 2895, question.QuestionNumber, 2895, 24, false);

#line default
#line hidden
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2941, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 88 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
							}


						}

#line default
#line hidden
                BeginContext(2966, 27, true);
                WriteLiteral("\t\t\t\t\t</select>\r\n\t\t\t\t\t<br>\r\n");
                EndContext();
#line 94 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"

				}

			}
		

#line default
#line hidden
                BeginContext(3015, 9, true);
                WriteLiteral("\t</div>\r\n");
                EndContext();
#line 100 "C:\Users\New\gges-main\programming-project\GitHub\majorProjectFrontEnd\MajorProjectFrontEnd\Views\Home\Index.cshtml"
}

#line default
#line hidden
                BeginContext(3027, 56, true);
                WriteLiteral("\t<p id=\"mainErrorMessage\"></p>\r\n\t<input type=\"submit\">\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3090, 18, true);
            WriteLiteral("\r\n</article>\r\n\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<QuestionDataModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
