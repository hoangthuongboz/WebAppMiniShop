#pragma checksum "D:\Shop\WebAppShopFull\WebApp\Views\Shared\_LayoutAdmin.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f37cce5e3efcbefb248bf404291225fafd8fa032"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__LayoutAdmin), @"mvc.1.0.view", @"/Views/Shared/_LayoutAdmin.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_LayoutAdmin.cshtml", typeof(AspNetCore.Views_Shared__LayoutAdmin))]
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
#line 1 "D:\Shop\WebAppShopFull\WebApp\Views\_ViewImports.cshtml"
using DTO;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f37cce5e3efcbefb248bf404291225fafd8fa032", @"/Views/Shared/_LayoutAdmin.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"75616952ad48f557dea3eb4b5ab9331af75af777", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__LayoutAdmin : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 25, true);
            WriteLiteral("<!DOCTYPE html>\r\n<html>\r\n");
            EndContext();
            BeginContext(25, 252, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f37cce5e3efcbefb248bf404291225fafd8fa0323200", async() => {
                BeginContext(31, 72, true);
                WriteLiteral("\r\n    <meta name=\"viewport\" content=\"width=device-width\" />\r\n    <title>");
                EndContext();
                BeginContext(104, 13, false);
#line 5 "D:\Shop\WebAppShopFull\WebApp\Views\Shared\_LayoutAdmin.cshtml"
      Write(ViewBag.Title);

#line default
#line hidden
                EndContext();
                BeginContext(117, 153, true);
                WriteLiteral("</title>\r\n    <link href=\"/css/style.css\" rel=\"stylesheet\" />\r\n    <link href=\"/css/all.css\" rel=\"stylesheet\" />\r\n    <script src=\"/js/js.js\"></script>\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(277, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(279, 1107, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f37cce5e3efcbefb248bf404291225fafd8fa0324954", async() => {
                BeginContext(285, 243, true);
                WriteLiteral("\r\n    <div class=\"title\">\r\n        <div class=\"container\">\r\n            <div class=\"container_title\">\r\n                <a href=\"/\"><h2>SportsShop</h2></a>\r\n            </div>\r\n            <div class=\"container_icon\" style=\"margin-top:20px;\">\r\n");
                EndContext();
#line 17 "D:\Shop\WebAppShopFull\WebApp\Views\Shared\_LayoutAdmin.cshtml"
                 if (User.Identity.IsAuthenticated)
                {

#line default
#line hidden
                BeginContext(600, 102, true);
                WriteLiteral("                    <a href=\"/auth/signout\">LogOut</a>\r\n                    <a href=\"/auth/\">Welcome: ");
                EndContext();
                BeginContext(703, 18, false);
#line 20 "D:\Shop\WebAppShopFull\WebApp\Views\Shared\_LayoutAdmin.cshtml"
                                         Write(User.Identity.Name);

#line default
#line hidden
                EndContext();
                BeginContext(721, 6, true);
                WriteLiteral("</a>\r\n");
                EndContext();
#line 21 "D:\Shop\WebAppShopFull\WebApp\Views\Shared\_LayoutAdmin.cshtml"
                }
                else
                {

#line default
#line hidden
                BeginContext(787, 45, true);
                WriteLiteral("                    <a href=\"/auth/signup\">\r\n");
                EndContext();
                BeginContext(1010, 160, true);
                WriteLiteral("                        SignUp\r\n                    </a>\r\n                    <a href=\"/auth/signin\">\r\n                        Login\r\n                    </a>\r\n");
                EndContext();
#line 33 "D:\Shop\WebAppShopFull\WebApp\Views\Shared\_LayoutAdmin.cshtml"
                }

#line default
#line hidden
                BeginContext(1189, 67, true);
                WriteLiteral("            </div>\r\n        </div>\r\n    </div>\r\n    <div>\r\n        ");
                EndContext();
                BeginContext(1257, 12, false);
#line 38 "D:\Shop\WebAppShopFull\WebApp\Views\Shared\_LayoutAdmin.cshtml"
   Write(RenderBody());

#line default
#line hidden
                EndContext();
                BeginContext(1269, 110, true);
                WriteLiteral("\r\n    </div>\r\n    <div class=\"copyright\">\r\n        <div class=\"container\">CopyRight &copy;</div>\r\n    </div>\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1386, 11, true);
            WriteLiteral("\r\n</html>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
