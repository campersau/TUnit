"use strict";(self.webpackChunktunit_docs_site=self.webpackChunktunit_docs_site||[]).push([[9069],{1075:(e,t,s)=>{s.r(t),s.d(t,{assets:()=>c,contentTitle:()=>r,default:()=>d,frontMatter:()=>a,metadata:()=>o,toc:()=>l});var n=s(4848),i=s(8453);const a={sidebar_position:6},r="Matrix Tests",o={id:"tutorial-basics/matrix-tests",title:"Matrix Tests",description:"Combinative tests can take multiple values for different arguments, and then generate every possible combination of all of those arguments.",source:"@site/docs/tutorial-basics/matrix-tests.md",sourceDirName:"tutorial-basics",slug:"/tutorial-basics/matrix-tests",permalink:"/TUnit/docs/tutorial-basics/matrix-tests",draft:!1,unlisted:!1,tags:[],version:"current",sidebarPosition:6,frontMatter:{sidebar_position:6},sidebar:"tutorialSidebar",previous:{title:"Data Source Driven Tests",permalink:"/TUnit/docs/tutorial-basics/data-source-driven-tests"},next:{title:"Things to know",permalink:"/TUnit/docs/tutorial-basics/things-to-know"}},c={},l=[];function u(e){const t={code:"code",h1:"h1",header:"header",p:"p",pre:"pre",...(0,i.R)(),...e.components};return(0,n.jsxs)(n.Fragment,{children:[(0,n.jsx)(t.header,{children:(0,n.jsx)(t.h1,{id:"matrix-tests",children:"Matrix Tests"})}),"\n",(0,n.jsx)(t.p,{children:"Combinative tests can take multiple values for different arguments, and then generate every possible combination of all of those arguments."}),"\n",(0,n.jsx)(t.p,{children:"Now bear in mind, that as your number of arguments and/or parameters increase, that the number of test cases will grow exponentially. This means you could very quickly get into the territory of generating thousands of test cases. So use it with caution."}),"\n",(0,n.jsxs)(t.p,{children:["For our arguments, we'll add a ",(0,n.jsx)(t.code,{children:"[Matrix]"})," attribute. Instead of this being added to the test method, it's added to the parameters themselves."]}),"\n",(0,n.jsx)(t.p,{children:"Here's an example:"}),"\n",(0,n.jsx)(t.pre,{children:(0,n.jsx)(t.code,{className:"language-csharp",children:"using TUnit.Assertions;\nusing TUnit.Assertions.Extensions;\nusing TUnit.Assertions.Extensions.Is;\nusing TUnit.Core;\n\nnamespace MyTestProject;\n\npublic class MyTestClass\n{\n    [Test]\n    public async Task MyTest(\n        [Matrix(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)] int value1,\n        [Matrix(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)] int value2\n        )\n    {\n        var result = Add(value1, value2);\n\n        await Assert.That(result).IsPositive();\n    }\n\n    private int Add(int x, int y)\n    {\n        return x + y;\n    }\n}\n"})}),"\n",(0,n.jsx)(t.p,{children:"That will generate 100 test cases. 10 different values for value1, and 10 different values for value2. 10*10 is 100."})]})}function d(e={}){const{wrapper:t}={...(0,i.R)(),...e.components};return t?(0,n.jsx)(t,{...e,children:(0,n.jsx)(u,{...e})}):u(e)}},8453:(e,t,s)=>{s.d(t,{R:()=>r,x:()=>o});var n=s(6540);const i={},a=n.createContext(i);function r(e){const t=n.useContext(a);return n.useMemo((function(){return"function"==typeof e?e(t):{...t,...e}}),[t,e])}function o(e){let t;return t=e.disableParentContext?"function"==typeof e.components?e.components(i):e.components||i:r(e.components),n.createElement(a.Provider,{value:t},e.children)}}}]);