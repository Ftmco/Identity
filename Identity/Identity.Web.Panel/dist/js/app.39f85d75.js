(function(e){function n(n){for(var r,o,a=n[0],i=n[1],l=n[2],d=0,f=[];d<a.length;d++)o=a[d],Object.prototype.hasOwnProperty.call(u,o)&&u[o]&&f.push(u[o][0]),u[o]=0;for(r in i)Object.prototype.hasOwnProperty.call(i,r)&&(e[r]=i[r]);s&&s(n);while(f.length)f.shift()();return c.push.apply(c,l||[]),t()}function t(){for(var e,n=0;n<c.length;n++){for(var t=c[n],r=!0,o=1;o<t.length;o++){var a=t[o];0!==u[a]&&(r=!1)}r&&(c.splice(n--,1),e=i(i.s=t[0]))}return e}var r={},o={app:0},u={app:0},c=[];function a(e){return i.p+"js/"+({}[e]||e)+"."+{"chunk-44de758d":"81f36ed3","chunk-560d247c":"df11f6d9","chunk-d3cb53b2":"7a81b246","chunk-48984714":"180a46dd","chunk-180705d9":"541b1799","chunk-574ec896":"2c0ccc4f","chunk-ded5655a":"46eeef94","chunk-250a6704":"bfc0946e"}[e]+".js"}function i(n){if(r[n])return r[n].exports;var t=r[n]={i:n,l:!1,exports:{}};return e[n].call(t.exports,t,t.exports,i),t.l=!0,t.exports}i.e=function(e){var n=[],t={"chunk-d3cb53b2":1,"chunk-48984714":1,"chunk-180705d9":1,"chunk-574ec896":1,"chunk-ded5655a":1,"chunk-250a6704":1};o[e]?n.push(o[e]):0!==o[e]&&t[e]&&n.push(o[e]=new Promise((function(n,t){for(var r="css/"+({}[e]||e)+"."+{"chunk-44de758d":"31d6cfe0","chunk-560d247c":"31d6cfe0","chunk-d3cb53b2":"4aa4e90e","chunk-48984714":"25f09c6b","chunk-180705d9":"31223dbd","chunk-574ec896":"3b95497d","chunk-ded5655a":"3db87338","chunk-250a6704":"03277372"}[e]+".css",u=i.p+r,c=document.getElementsByTagName("link"),a=0;a<c.length;a++){var l=c[a],d=l.getAttribute("data-href")||l.getAttribute("href");if("stylesheet"===l.rel&&(d===r||d===u))return n()}var f=document.getElementsByTagName("style");for(a=0;a<f.length;a++){l=f[a],d=l.getAttribute("data-href");if(d===r||d===u)return n()}var s=document.createElement("link");s.rel="stylesheet",s.type="text/css",s.onload=n,s.onerror=function(n){var r=n&&n.target&&n.target.src||u,c=new Error("Loading CSS chunk "+e+" failed.\n("+r+")");c.code="CSS_CHUNK_LOAD_FAILED",c.request=r,delete o[e],s.parentNode.removeChild(s),t(c)},s.href=u;var h=document.getElementsByTagName("head")[0];h.appendChild(s)})).then((function(){o[e]=0})));var r=u[e];if(0!==r)if(r)n.push(r[2]);else{var c=new Promise((function(n,t){r=u[e]=[n,t]}));n.push(r[2]=c);var l,d=document.createElement("script");d.charset="utf-8",d.timeout=120,i.nc&&d.setAttribute("nonce",i.nc),d.src=a(e);var f=new Error;l=function(n){d.onerror=d.onload=null,clearTimeout(s);var t=u[e];if(0!==t){if(t){var r=n&&("load"===n.type?"missing":n.type),o=n&&n.target&&n.target.src;f.message="Loading chunk "+e+" failed.\n("+r+": "+o+")",f.name="ChunkLoadError",f.type=r,f.request=o,t[1](f)}u[e]=void 0}};var s=setTimeout((function(){l({type:"timeout",target:d})}),12e4);d.onerror=d.onload=l,document.head.appendChild(d)}return Promise.all(n)},i.m=e,i.c=r,i.d=function(e,n,t){i.o(e,n)||Object.defineProperty(e,n,{enumerable:!0,get:t})},i.r=function(e){"undefined"!==typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},i.t=function(e,n){if(1&n&&(e=i(e)),8&n)return e;if(4&n&&"object"===typeof e&&e&&e.__esModule)return e;var t=Object.create(null);if(i.r(t),Object.defineProperty(t,"default",{enumerable:!0,value:e}),2&n&&"string"!=typeof e)for(var r in e)i.d(t,r,function(n){return e[n]}.bind(null,r));return t},i.n=function(e){var n=e&&e.__esModule?function(){return e["default"]}:function(){return e};return i.d(n,"a",n),n},i.o=function(e,n){return Object.prototype.hasOwnProperty.call(e,n)},i.p="/",i.oe=function(e){throw console.error(e),e};var l=window["webpackJsonp"]=window["webpackJsonp"]||[],d=l.push.bind(l);l.push=n,l=l.slice();for(var f=0;f<l.length;f++)n(l[f]);var s=d;c.push([0,"chunk-vendors"]),t()})({0:function(e,n,t){e.exports=t("cd49")},cd49:function(e,n,t){"use strict";t.r(n);t("e260"),t("e6cf"),t("cca6"),t("a79d");var r=t("2b0e"),o=(t("d3b7"),t("bc3a")),u=t.n(o),c={},a=u.a.create(c);a.interceptors.request.use((function(e){return e}),(function(e){return Promise.reject(e)})),a.interceptors.response.use((function(e){return e}),(function(e){return Promise.reject(e)})),Plugin.install=function(e,n){e.axios=a,window.axios=a,Object.defineProperties(e.prototype,{axios:{get:function(){return a}},$axios:{get:function(){return a}}})},r["a"].use(Plugin);Plugin;var i=function(){var e=this,n=e.$createElement,t=e._self._c||n;return t("v-app",[t("router-view")],1)},l=[],d=r["a"].extend({name:"App",data:function(){return{}}}),f=d,s=t("2877"),h=t("6544"),p=t.n(h),m=t("7496"),b=Object(s["a"])(f,i,l,!1,null,null,null),g=b.exports;p()(b,{VApp:m["a"]});var v=t("f309");r["a"].use(v["a"]);var k=new v["a"]({}),y=t("5530"),w=t("53ca"),P=(t("3ca3"),t("ddb0"),t("8c4f")),j=function(e){var n=e.store,t=e.next;return n.getters.auth.isLogin?t({name:"Profile"}):t()},x=t("2f62"),O=t("fc08");r["a"].use(x["a"]);var A=new x["a"].Store({state:{user:{isLogin:O["a"].isAuthenticated()}},getters:{auth:function(e){return e.user}},mutations:{},actions:{},modules:{}}),E=function(e,n,t){var r=n[t];return r?function(){var o=pipeline(e,n,t+1);r(Object(y["a"])(Object(y["a"])({},e),{},{next:o}))}:e.next},L=function(e){var n=e.store,t=e.next;return n.getters.auth.isLogin?t():t({name:"Login"})};r["a"].use(P["a"]);var S=[{path:"/",component:function(){return Promise.all([t.e("chunk-d3cb53b2"),t.e("chunk-574ec896"),t.e("chunk-250a6704")]).then(t.bind(null,"ae0a"))},meta:function(e){return{route:e,title:"Home"}},children:[{path:"",name:"Home",component:function(){return t.e("chunk-560d247c").then(t.bind(null,"f5b8"))},meta:function(e){return{route:e,title:"Home"}}},{path:"/application",name:"Application",component:function(){return Promise.all([t.e("chunk-d3cb53b2"),t.e("chunk-48984714"),t.e("chunk-574ec896"),t.e("chunk-ded5655a")]).then(t.bind(null,"3e6e"))},meta:function(e){return{route:e,title:"Application"}}},{path:"/account/login",name:"Login",component:function(){return Promise.all([t.e("chunk-d3cb53b2"),t.e("chunk-48984714"),t.e("chunk-180705d9")]).then(t.bind(null,"8db0"))},meta:function(e){return{route:e,title:"Login",middleware:j}}},{path:"/account/profile",name:"Profile",component:function(){return t.e("chunk-44de758d").then(t.bind(null,"7a38"))},meta:function(e){return{route:e,title:"Profile",middleware:L}}}]}],_=new P["a"]({routes:S,mode:"history"});_.beforeEach((function(e,n,t){var r=e.meta(e);if(console.log(Object(w["a"])(e.meta)),!r.middleware)return t();var o=r.middleware,u={to:e,from:n,next:t,store:A};return o(Object(y["a"])(Object(y["a"])({},u),{},{next:E(u,o,1)}))}));var T=_;r["a"].config.productionTip=!0,new r["a"]({vuetify:k,router:T,render:function(e){return e(g)}}).$mount("#app")},fc08:function(e,n,t){"use strict";n["a"]={authenticationToken:function(){return localStorage.getItem("I-Authentication")},isAuthenticated:function(){var e=this.authenticationToken();return null!=e}}}});
//# sourceMappingURL=app.39f85d75.js.map