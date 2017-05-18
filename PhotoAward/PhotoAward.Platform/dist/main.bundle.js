webpackJsonp([1,4],{

/***/ 157:
/***/ (function(module, exports) {

function webpackEmptyContext(req) {
	throw new Error("Cannot find module '" + req + "'.");
}
webpackEmptyContext.keys = function() { return []; };
webpackEmptyContext.resolve = webpackEmptyContext;
module.exports = webpackEmptyContext;
webpackEmptyContext.id = 157;


/***/ }),

/***/ 158:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser_dynamic__ = __webpack_require__(164);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__(6);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__environments_environment__ = __webpack_require__(168);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__app_app_module__ = __webpack_require__(167);




if (__WEBPACK_IMPORTED_MODULE_2__environments_environment__["a" /* environment */].production) {
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["a" /* enableProdMode */])();
}
__webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_platform_browser_dynamic__["a" /* platformBrowserDynamic */])().bootstrapModule(__WEBPACK_IMPORTED_MODULE_3__app_app_module__["a" /* AppModule */]);
//# sourceMappingURL=C:/Projekte/tmbit/photoaward/PhotoAward/PhotoAward.AngularClient/src/main.js.map

/***/ }),

/***/ 165:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__photos_show_photos_component_show_photos_component_component__ = __webpack_require__(96);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__photos_upload_photo_component_upload_photo_component_component__ = __webpack_require__(97);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__login_login_login_component__ = __webpack_require__(94);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__login_register_member_register_member_component__ = __webpack_require__(95);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_core__ = __webpack_require__(6);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__angular_router__ = __webpack_require__(46);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppRoutingModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};






var routes = [{ path: '', component: __WEBPACK_IMPORTED_MODULE_2__login_login_login_component__["a" /* LoginComponent */], pathMatch: 'full' },
    { path: 'registerMember', component: __WEBPACK_IMPORTED_MODULE_3__login_register_member_register_member_component__["a" /* RegisterMemberComponent */] },
    { path: 'upload', component: __WEBPACK_IMPORTED_MODULE_1__photos_upload_photo_component_upload_photo_component_component__["a" /* UploadPhotoComponentComponent */] },
    { path: 'gallery', component: __WEBPACK_IMPORTED_MODULE_0__photos_show_photos_component_show_photos_component_component__["a" /* ShowPhotosComponentComponent */] }
];
var AppRoutingModule = (function () {
    function AppRoutingModule() {
    }
    return AppRoutingModule;
}());
AppRoutingModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_4__angular_core__["c" /* NgModule */])({
        imports: [__WEBPACK_IMPORTED_MODULE_5__angular_router__["a" /* RouterModule */].forRoot(routes)],
        exports: [__WEBPACK_IMPORTED_MODULE_5__angular_router__["a" /* RouterModule */]],
        providers: []
    })
], AppRoutingModule);

//# sourceMappingURL=C:/Projekte/tmbit/photoaward/PhotoAward/PhotoAward.AngularClient/src/app-routing.module.js.map

/***/ }),

/***/ 166:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__Shared_uploadService__ = __webpack_require__(62);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__Shared_Controllers_generated__ = __webpack_require__(28);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__(6);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__Shared_user_service__ = __webpack_require__(33);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var AppComponent = (function () {
    function AppComponent(userservice, _photoManagementClient, _uploadService) {
        this.userservice = userservice;
        this._photoManagementClient = _photoManagementClient;
        this._uploadService = _uploadService;
        this.title = 'Photo Award Client';
        this.email = '';
        this.notMember = true;
        this.firstname = '';
        this.surname = '';
    }
    AppComponent.prototype.imageUploaded = function (message) {
        var e = this.email;
        this.email = "";
        this.email = e;
    };
    return AppComponent;
}());
AppComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2__angular_core__["_14" /* Component */])({
        selector: 'pac-root',
        template: __webpack_require__(323),
        providers: [],
        styles: []
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_3__Shared_user_service__["a" /* UserService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__Shared_user_service__["a" /* UserService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_1__Shared_Controllers_generated__["a" /* PhotoManagementClient */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__Shared_Controllers_generated__["a" /* PhotoManagementClient */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_0__Shared_uploadService__["a" /* UploadService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__Shared_uploadService__["a" /* UploadService */]) === "function" && _c || Object])
], AppComponent);

var _a, _b, _c;
//# sourceMappingURL=C:/Projekte/tmbit/photoaward/PhotoAward/PhotoAward.AngularClient/src/app.component.js.map

/***/ }),

/***/ 167:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_app_Shared_user_service__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__photos_show_photos_component_show_photos_component_component__ = __webpack_require__(96);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__photos_upload_photo_component_upload_photo_component_component__ = __webpack_require__(97);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__login_register_member_register_member_component__ = __webpack_require__(95);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__Shared_Controllers_generated__ = __webpack_require__(28);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__Shared_tokenservice__ = __webpack_require__(92);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__Shared_uploadService__ = __webpack_require__(62);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__angular_platform_browser__ = __webpack_require__(38);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__angular_core__ = __webpack_require__(6);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__angular_forms__ = __webpack_require__(163);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__angular_http__ = __webpack_require__(61);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__app_routing_module__ = __webpack_require__(165);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_12__app_component__ = __webpack_require__(166);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_13__login_login_login_component__ = __webpack_require__(94);
/* unused harmony export DefaultRequestOptions */
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppModule; });
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};















var API_BASE_URL2 = 'http://NB-000953:8200';
if (window != null && window.location != null) {
    var loc = window.location;
    var server = loc.hostname;
    var port = loc.port;
    if (server === 'localhost' && port === '4200') {
        port = '8200';
    }
    API_BASE_URL2 = loc.protocol + '//' + server + ':' + port;
}
var DefaultRequestOptions = (function (_super) {
    __extends(DefaultRequestOptions, _super);
    function DefaultRequestOptions(_userService) {
        var _this = _super.call(this) || this;
        _this._userService = _userService;
        _this.headers = new __WEBPACK_IMPORTED_MODULE_10__angular_http__["a" /* Headers */]({
            'Author': 'BridgingIT GmbH',
        });
        return _this;
    }
    DefaultRequestOptions.prototype.merge = function (options) {
        var newOptions = _super.prototype.merge.call(this, options);
        if (this._userService.token) {
            newOptions.headers.set('Authorization', 'Bearer ' + this._userService.token.access_token);
        }
        else {
            newOptions.headers.delete('bearer');
        }
        return newOptions;
    };
    return DefaultRequestOptions;
}(__WEBPACK_IMPORTED_MODULE_10__angular_http__["b" /* BaseRequestOptions */]));
DefaultRequestOptions = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_8__angular_core__["b" /* Injectable */])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0_app_Shared_user_service__["a" /* UserService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0_app_Shared_user_service__["a" /* UserService */]) === "function" && _a || Object])
], DefaultRequestOptions);

var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_8__angular_core__["c" /* NgModule */])({
        declarations: [
            __WEBPACK_IMPORTED_MODULE_12__app_component__["a" /* AppComponent */],
            __WEBPACK_IMPORTED_MODULE_3__login_register_member_register_member_component__["a" /* RegisterMemberComponent */],
            __WEBPACK_IMPORTED_MODULE_2__photos_upload_photo_component_upload_photo_component_component__["a" /* UploadPhotoComponentComponent */],
            __WEBPACK_IMPORTED_MODULE_1__photos_show_photos_component_show_photos_component_component__["a" /* ShowPhotosComponentComponent */],
            __WEBPACK_IMPORTED_MODULE_13__login_login_login_component__["a" /* LoginComponent */]
        ],
        imports: [
            __WEBPACK_IMPORTED_MODULE_7__angular_platform_browser__["a" /* BrowserModule */],
            __WEBPACK_IMPORTED_MODULE_9__angular_forms__["a" /* FormsModule */],
            __WEBPACK_IMPORTED_MODULE_10__angular_http__["c" /* HttpModule */],
            __WEBPACK_IMPORTED_MODULE_11__app_routing_module__["a" /* AppRoutingModule */]
        ],
        providers: [
            __WEBPACK_IMPORTED_MODULE_4__Shared_Controllers_generated__["a" /* PhotoManagementClient */],
            __WEBPACK_IMPORTED_MODULE_4__Shared_Controllers_generated__["b" /* MemberManagementClient */],
            __WEBPACK_IMPORTED_MODULE_6__Shared_uploadService__["a" /* UploadService */],
            __WEBPACK_IMPORTED_MODULE_5__Shared_tokenservice__["a" /* TokenService */],
            __WEBPACK_IMPORTED_MODULE_0_app_Shared_user_service__["a" /* UserService */],
            { provide: __WEBPACK_IMPORTED_MODULE_4__Shared_Controllers_generated__["c" /* API_BASE_URL */], useValue: API_BASE_URL2 },
            { provide: __WEBPACK_IMPORTED_MODULE_5__Shared_tokenservice__["b" /* API_BASE_URL */], useValue: API_BASE_URL2 },
            { provide: __WEBPACK_IMPORTED_MODULE_10__angular_http__["d" /* RequestOptions */], useClass: DefaultRequestOptions }
        ],
        bootstrap: [__WEBPACK_IMPORTED_MODULE_12__app_component__["a" /* AppComponent */]]
    })
], AppModule);

var _a;
//# sourceMappingURL=C:/Projekte/tmbit/photoaward/PhotoAward/PhotoAward.AngularClient/src/app.module.js.map

/***/ }),

/***/ 168:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return environment; });
// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `angular-cli.json`.
// The file contents for the current environment will overwrite these during build.
var environment = {
    production: false
};
//# sourceMappingURL=C:/Projekte/tmbit/photoaward/PhotoAward/PhotoAward.AngularClient/src/environment.js.map

/***/ }),

/***/ 28:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_rxjs_add_observable_fromPromise__ = __webpack_require__(138);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_rxjs_add_observable_fromPromise___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_0_rxjs_add_observable_fromPromise__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_add_observable_of__ = __webpack_require__(139);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_add_observable_of___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_rxjs_add_observable_of__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_rxjs_add_observable_throw__ = __webpack_require__(140);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_rxjs_add_observable_throw___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_rxjs_add_observable_throw__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_rxjs_add_operator_map__ = __webpack_require__(142);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_rxjs_add_operator_map___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_3_rxjs_add_operator_map__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_rxjs_add_operator_toPromise__ = __webpack_require__(144);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_rxjs_add_operator_toPromise___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_4_rxjs_add_operator_toPromise__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5_rxjs_add_operator_mergeMap__ = __webpack_require__(143);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5_rxjs_add_operator_mergeMap___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_5_rxjs_add_operator_mergeMap__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6_rxjs_add_operator_catch__ = __webpack_require__(141);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6_rxjs_add_operator_catch___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_6_rxjs_add_operator_catch__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__ = __webpack_require__(3);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__angular_core__ = __webpack_require__(6);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__angular_http__ = __webpack_require__(61);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "c", function() { return API_BASE_URL; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "b", function() { return MemberManagementClient; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return PhotoManagementClient; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "d", function() { return MemberDto; });
/* unused harmony export ChangePasswordDto */
/* unused harmony export PhotoUploadData */
/* unused harmony export PhotoManagementData */
/* unused harmony export PhotoMemberInfo */
/* unused harmony export CommentData */
/* unused harmony export CommentUploadData */
/* unused harmony export SwaggerException */
/* tslint:disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v10.5.6320.26089 (NJsonSchema v8.32.6319.16936) (http://NSwag.org)
// </auto-generated>
//----------------------
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};










var API_BASE_URL = new __WEBPACK_IMPORTED_MODULE_8__angular_core__["Y" /* OpaqueToken */]('API_BASE_URL');
var MemberManagementClient = (function () {
    function MemberManagementClient(http, baseUrl) {
        this.jsonParseReviver = undefined;
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }
    MemberManagementClient.prototype.add = function (member) {
        var _this = this;
        var url_ = this.baseUrl + "/api/Member/Add";
        url_ = url_.replace(/[?&]$/, "");
        var content_ = JSON.stringify(member ? member.toJSON() : null);
        var options_ = {
            body: content_,
            method: "post",
            headers: new __WEBPACK_IMPORTED_MODULE_9__angular_http__["a" /* Headers */]({
                "Content-Type": "application/json; charset=UTF-8",
                "Accept": "application/json; charset=UTF-8"
            })
        };
        return this.http.request(url_, options_).flatMap(function (response_) {
            return _this.processAdd(response_);
        }).catch(function (response_) {
            if (response_ instanceof __WEBPACK_IMPORTED_MODULE_9__angular_http__["e" /* Response */]) {
                try {
                    return _this.processAdd(response_);
                }
                catch (e) {
                    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(e);
                }
            }
            else
                return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(response_);
        });
    };
    MemberManagementClient.prototype.processAdd = function (response) {
        var status = response.status;
        if (status === 200) {
            var responseText = response.text();
            var result200 = null;
            var resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            result200 = resultData200 ? MemberDto.fromJS(resultData200) : null;
            return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(result200);
        }
        else if (status !== 200 && status !== 204) {
            var responseText = response.text();
            return throwException("An unexpected server error occurred.", status, responseText);
        }
        return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
    };
    MemberManagementClient.prototype.get = function (email) {
        var _this = this;
        var url_ = this.baseUrl + "/api/Member/Get/{email}";
        if (email === undefined || email === null)
            throw new Error("The parameter 'email' must be defined.");
        url_ = url_.replace("{email}", encodeURIComponent("" + email));
        url_ = url_.replace(/[?&]$/, "");
        var content_ = "";
        var options_ = {
            body: content_,
            method: "get",
            headers: new __WEBPACK_IMPORTED_MODULE_9__angular_http__["a" /* Headers */]({
                "Content-Type": "application/json; charset=UTF-8",
                "Accept": "application/json; charset=UTF-8"
            })
        };
        return this.http.request(url_, options_).flatMap(function (response_) {
            return _this.processGet(response_);
        }).catch(function (response_) {
            if (response_ instanceof __WEBPACK_IMPORTED_MODULE_9__angular_http__["e" /* Response */]) {
                try {
                    return _this.processGet(response_);
                }
                catch (e) {
                    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(e);
                }
            }
            else
                return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(response_);
        });
    };
    MemberManagementClient.prototype.processGet = function (response) {
        var status = response.status;
        if (status === 200) {
            var responseText = response.text();
            var result200 = null;
            var resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            result200 = resultData200 ? MemberDto.fromJS(resultData200) : null;
            return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(result200);
        }
        else if (status !== 200 && status !== 204) {
            var responseText = response.text();
            return throwException("An unexpected server error occurred.", status, responseText);
        }
        return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
    };
    MemberManagementClient.prototype.login = function (email, password) {
        var _this = this;
        var url_ = this.baseUrl + "/api/Member/Login?";
        if (email === undefined)
            throw new Error("The parameter 'email' must be defined.");
        else
            url_ += "email=" + encodeURIComponent("" + email) + "&";
        if (password === undefined)
            throw new Error("The parameter 'password' must be defined.");
        else
            url_ += "password=" + encodeURIComponent("" + password) + "&";
        url_ = url_.replace(/[?&]$/, "");
        var content_ = "";
        var options_ = {
            body: content_,
            method: "post",
            headers: new __WEBPACK_IMPORTED_MODULE_9__angular_http__["a" /* Headers */]({
                "Content-Type": "application/json; charset=UTF-8",
                "Accept": "application/json; charset=UTF-8"
            })
        };
        return this.http.request(url_, options_).flatMap(function (response_) {
            return _this.processLogin(response_);
        }).catch(function (response_) {
            if (response_ instanceof __WEBPACK_IMPORTED_MODULE_9__angular_http__["e" /* Response */]) {
                try {
                    return _this.processLogin(response_);
                }
                catch (e) {
                    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(e);
                }
            }
            else
                return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(response_);
        });
    };
    MemberManagementClient.prototype.processLogin = function (response) {
        var status = response.status;
        if (status === 200) {
            var responseText = response.text();
            var result200 = null;
            var resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            result200 = resultData200 ? MemberDto.fromJS(resultData200) : null;
            return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(result200);
        }
        else if (status !== 200 && status !== 204) {
            var responseText = response.text();
            return throwException("An unexpected server error occurred.", status, responseText);
        }
        return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
    };
    MemberManagementClient.prototype.changePassword = function (dto) {
        var _this = this;
        var url_ = this.baseUrl + "/api/Member/ChangePassword";
        url_ = url_.replace(/[?&]$/, "");
        var content_ = JSON.stringify(dto ? dto.toJSON() : null);
        var options_ = {
            body: content_,
            method: "post",
            headers: new __WEBPACK_IMPORTED_MODULE_9__angular_http__["a" /* Headers */]({
                "Content-Type": "application/json; charset=UTF-8",
                "Accept": "application/json; charset=UTF-8"
            })
        };
        return this.http.request(url_, options_).flatMap(function (response_) {
            return _this.processChangePassword(response_);
        }).catch(function (response_) {
            if (response_ instanceof __WEBPACK_IMPORTED_MODULE_9__angular_http__["e" /* Response */]) {
                try {
                    return _this.processChangePassword(response_);
                }
                catch (e) {
                    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(e);
                }
            }
            else
                return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(response_);
        });
    };
    MemberManagementClient.prototype.processChangePassword = function (response) {
        var status = response.status;
        if (status === 200) {
            var responseText = response.text();
            var result200 = null;
            var resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            result200 = resultData200 ? MemberDto.fromJS(resultData200) : null;
            return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(result200);
        }
        else if (status !== 200 && status !== 204) {
            var responseText = response.text();
            return throwException("An unexpected server error occurred.", status, responseText);
        }
        return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
    };
    return MemberManagementClient;
}());
MemberManagementClient = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_8__angular_core__["b" /* Injectable */])(),
    __param(0, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_8__angular_core__["g" /* Inject */])(__WEBPACK_IMPORTED_MODULE_9__angular_http__["f" /* Http */])), __param(1, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_8__angular_core__["m" /* Optional */])()), __param(1, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_8__angular_core__["g" /* Inject */])(API_BASE_URL)),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_9__angular_http__["f" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_9__angular_http__["f" /* Http */]) === "function" && _a || Object, String])
], MemberManagementClient);

var PhotoManagementClient = (function () {
    function PhotoManagementClient(http, baseUrl) {
        this.jsonParseReviver = undefined;
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }
    PhotoManagementClient.prototype.add = function (uploadData) {
        var _this = this;
        var url_ = this.baseUrl + "/api/Photo/Add";
        url_ = url_.replace(/[?&]$/, "");
        var content_ = JSON.stringify(uploadData ? uploadData.toJSON() : null);
        var options_ = {
            body: content_,
            method: "post",
            headers: new __WEBPACK_IMPORTED_MODULE_9__angular_http__["a" /* Headers */]({
                "Content-Type": "application/json; charset=UTF-8",
                "Accept": "application/json; charset=UTF-8"
            })
        };
        return this.http.request(url_, options_).flatMap(function (response_) {
            return _this.processAdd(response_);
        }).catch(function (response_) {
            if (response_ instanceof __WEBPACK_IMPORTED_MODULE_9__angular_http__["e" /* Response */]) {
                try {
                    return _this.processAdd(response_);
                }
                catch (e) {
                    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(e);
                }
            }
            else
                return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(response_);
        });
    };
    PhotoManagementClient.prototype.processAdd = function (response) {
        var status = response.status;
        if (status === 200) {
            var responseText = response.text();
            var result200 = null;
            var resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            result200 = resultData200 ? PhotoManagementData.fromJS(resultData200) : null;
            return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(result200);
        }
        else if (status !== 200 && status !== 204) {
            var responseText = response.text();
            return throwException("An unexpected server error occurred.", status, responseText);
        }
        return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
    };
    PhotoManagementClient.prototype.delete = function (photoId) {
        var _this = this;
        var url_ = this.baseUrl + "/api/Photo/Delete?";
        if (photoId === undefined || photoId === null)
            throw new Error("The parameter 'photoId' must be defined and cannot be null.");
        else
            url_ += "photoId=" + encodeURIComponent("" + photoId) + "&";
        url_ = url_.replace(/[?&]$/, "");
        var content_ = "";
        var options_ = {
            body: content_,
            method: "post",
            headers: new __WEBPACK_IMPORTED_MODULE_9__angular_http__["a" /* Headers */]({
                "Content-Type": "application/json; charset=UTF-8",
                "Accept": "application/json; charset=UTF-8"
            })
        };
        return this.http.request(url_, options_).flatMap(function (response_) {
            return _this.processDelete(response_);
        }).catch(function (response_) {
            if (response_ instanceof __WEBPACK_IMPORTED_MODULE_9__angular_http__["e" /* Response */]) {
                try {
                    return _this.processDelete(response_);
                }
                catch (e) {
                    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(e);
                }
            }
            else
                return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(response_);
        });
    };
    PhotoManagementClient.prototype.processDelete = function (response) {
        var status = response.status;
        if (status === 204) {
            var responseText = response.text();
            return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
        }
        else if (status !== 200 && status !== 204) {
            var responseText = response.text();
            return throwException("An unexpected server error occurred.", status, responseText);
        }
        return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
    };
    PhotoManagementClient.prototype.get = function (id) {
        var _this = this;
        var url_ = this.baseUrl + "/api/Photo/Get/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");
        var content_ = "";
        var options_ = {
            body: content_,
            method: "get",
            headers: new __WEBPACK_IMPORTED_MODULE_9__angular_http__["a" /* Headers */]({
                "Content-Type": "application/json; charset=UTF-8",
                "Accept": "application/json; charset=UTF-8"
            })
        };
        return this.http.request(url_, options_).flatMap(function (response_) {
            return _this.processGet(response_);
        }).catch(function (response_) {
            if (response_ instanceof __WEBPACK_IMPORTED_MODULE_9__angular_http__["e" /* Response */]) {
                try {
                    return _this.processGet(response_);
                }
                catch (e) {
                    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(e);
                }
            }
            else
                return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(response_);
        });
    };
    PhotoManagementClient.prototype.processGet = function (response) {
        var status = response.status;
        if (status === 200) {
            var responseText = response.text();
            var result200 = null;
            var resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            result200 = resultData200 ? PhotoManagementData.fromJS(resultData200) : null;
            return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(result200);
        }
        else if (status !== 200 && status !== 204) {
            var responseText = response.text();
            return throwException("An unexpected server error occurred.", status, responseText);
        }
        return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
    };
    PhotoManagementClient.prototype.getThumbnailsOfMember = function (email) {
        var _this = this;
        var url_ = this.baseUrl + "/api/Photo/GetThumbnailsOfMember/{email}";
        if (email === undefined || email === null)
            throw new Error("The parameter 'email' must be defined.");
        url_ = url_.replace("{email}", encodeURIComponent("" + email));
        url_ = url_.replace(/[?&]$/, "");
        var content_ = "";
        var options_ = {
            body: content_,
            method: "get",
            headers: new __WEBPACK_IMPORTED_MODULE_9__angular_http__["a" /* Headers */]({
                "Content-Type": "application/json; charset=UTF-8",
                "Accept": "application/json; charset=UTF-8"
            })
        };
        return this.http.request(url_, options_).flatMap(function (response_) {
            return _this.processGetThumbnailsOfMember(response_);
        }).catch(function (response_) {
            if (response_ instanceof __WEBPACK_IMPORTED_MODULE_9__angular_http__["e" /* Response */]) {
                try {
                    return _this.processGetThumbnailsOfMember(response_);
                }
                catch (e) {
                    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(e);
                }
            }
            else
                return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(response_);
        });
    };
    PhotoManagementClient.prototype.processGetThumbnailsOfMember = function (response) {
        var status = response.status;
        if (status === 200) {
            var responseText = response.text();
            var result200 = null;
            var resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            if (resultData200 && resultData200.constructor === Array) {
                result200 = [];
                for (var _i = 0, resultData200_1 = resultData200; _i < resultData200_1.length; _i++) {
                    var item = resultData200_1[_i];
                    result200.push(PhotoManagementData.fromJS(item));
                }
            }
            return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(result200);
        }
        else if (status !== 200 && status !== 204) {
            var responseText = response.text();
            return throwException("An unexpected server error occurred.", status, responseText);
        }
        return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
    };
    PhotoManagementClient.prototype.getImagesOfMember = function () {
        var _this = this;
        var url_ = this.baseUrl + "/api/Photo/GetImagesOfMember";
        url_ = url_.replace(/[?&]$/, "");
        var content_ = "";
        var options_ = {
            body: content_,
            method: "get",
            headers: new __WEBPACK_IMPORTED_MODULE_9__angular_http__["a" /* Headers */]({
                "Content-Type": "application/json; charset=UTF-8",
                "Accept": "application/json; charset=UTF-8"
            })
        };
        return this.http.request(url_, options_).flatMap(function (response_) {
            return _this.processGetImagesOfMember(response_);
        }).catch(function (response_) {
            if (response_ instanceof __WEBPACK_IMPORTED_MODULE_9__angular_http__["e" /* Response */]) {
                try {
                    return _this.processGetImagesOfMember(response_);
                }
                catch (e) {
                    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(e);
                }
            }
            else
                return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(response_);
        });
    };
    PhotoManagementClient.prototype.processGetImagesOfMember = function (response) {
        var status = response.status;
        if (status === 200) {
            var responseText = response.text();
            var result200 = null;
            var resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            if (resultData200 && resultData200.constructor === Array) {
                result200 = [];
                for (var _i = 0, resultData200_2 = resultData200; _i < resultData200_2.length; _i++) {
                    var item = resultData200_2[_i];
                    result200.push(PhotoMemberInfo.fromJS(item));
                }
            }
            return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(result200);
        }
        else if (status !== 200 && status !== 204) {
            var responseText = response.text();
            return throwException("An unexpected server error occurred.", status, responseText);
        }
        return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
    };
    PhotoManagementClient.prototype.getComments = function (photoId) {
        var _this = this;
        var url_ = this.baseUrl + "/api/Photo/GetComments/{photoId}";
        if (photoId === undefined || photoId === null)
            throw new Error("The parameter 'photoId' must be defined.");
        url_ = url_.replace("{photoId}", encodeURIComponent("" + photoId));
        url_ = url_.replace(/[?&]$/, "");
        var content_ = "";
        var options_ = {
            body: content_,
            method: "get",
            headers: new __WEBPACK_IMPORTED_MODULE_9__angular_http__["a" /* Headers */]({
                "Content-Type": "application/json; charset=UTF-8",
                "Accept": "application/json; charset=UTF-8"
            })
        };
        return this.http.request(url_, options_).flatMap(function (response_) {
            return _this.processGetComments(response_);
        }).catch(function (response_) {
            if (response_ instanceof __WEBPACK_IMPORTED_MODULE_9__angular_http__["e" /* Response */]) {
                try {
                    return _this.processGetComments(response_);
                }
                catch (e) {
                    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(e);
                }
            }
            else
                return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(response_);
        });
    };
    PhotoManagementClient.prototype.processGetComments = function (response) {
        var status = response.status;
        if (status === 200) {
            var responseText = response.text();
            var result200 = null;
            var resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            if (resultData200 && resultData200.constructor === Array) {
                result200 = [];
                for (var _i = 0, resultData200_3 = resultData200; _i < resultData200_3.length; _i++) {
                    var item = resultData200_3[_i];
                    result200.push(CommentData.fromJS(item));
                }
            }
            return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(result200);
        }
        else if (status !== 200 && status !== 204) {
            var responseText = response.text();
            return throwException("An unexpected server error occurred.", status, responseText);
        }
        return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
    };
    PhotoManagementClient.prototype.addComment = function (uploadData) {
        var _this = this;
        var url_ = this.baseUrl + "/api/Photo/AddComment";
        url_ = url_.replace(/[?&]$/, "");
        var content_ = JSON.stringify(uploadData ? uploadData.toJSON() : null);
        var options_ = {
            body: content_,
            method: "post",
            headers: new __WEBPACK_IMPORTED_MODULE_9__angular_http__["a" /* Headers */]({
                "Content-Type": "application/json; charset=UTF-8",
                "Accept": "application/json; charset=UTF-8"
            })
        };
        return this.http.request(url_, options_).flatMap(function (response_) {
            return _this.processAddComment(response_);
        }).catch(function (response_) {
            if (response_ instanceof __WEBPACK_IMPORTED_MODULE_9__angular_http__["e" /* Response */]) {
                try {
                    return _this.processAddComment(response_);
                }
                catch (e) {
                    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(e);
                }
            }
            else
                return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(response_);
        });
    };
    PhotoManagementClient.prototype.processAddComment = function (response) {
        var status = response.status;
        if (status === 200) {
            var responseText = response.text();
            var result200 = null;
            var resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            result200 = resultData200 ? CommentData.fromJS(resultData200) : null;
            return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(result200);
        }
        else if (status !== 200 && status !== 204) {
            var responseText = response.text();
            return throwException("An unexpected server error occurred.", status, responseText);
        }
        return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
    };
    PhotoManagementClient.prototype.uploadPhoto = function () {
        var _this = this;
        var url_ = this.baseUrl + "/api/Photo/UploadPhoto";
        url_ = url_.replace(/[?&]$/, "");
        var content_ = "";
        var options_ = {
            body: content_,
            method: "post",
            responseType: __WEBPACK_IMPORTED_MODULE_9__angular_http__["g" /* ResponseContentType */].Blob,
            headers: new __WEBPACK_IMPORTED_MODULE_9__angular_http__["a" /* Headers */]({
                "Content-Type": "application/json; charset=UTF-8",
            })
        };
        return this.http.request(url_, options_).flatMap(function (response_) {
            return _this.processUploadPhoto(response_);
        }).catch(function (response_) {
            if (response_ instanceof __WEBPACK_IMPORTED_MODULE_9__angular_http__["e" /* Response */]) {
                try {
                    return _this.processUploadPhoto(response_);
                }
                catch (e) {
                    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(e);
                }
            }
            else
                return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(response_);
        });
    };
    PhotoManagementClient.prototype.processUploadPhoto = function (response) {
        var status = response.status;
        if (status === 200) {
            return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(response.blob());
        }
        else if (status !== 200 && status !== 204) {
            return blobToText(response.blob()).flatMap(function (responseText) {
                return throwException("An unexpected server error occurred.", status, responseText);
            });
        }
        return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
    };
    PhotoManagementClient.prototype.backup = function () {
        var _this = this;
        var url_ = this.baseUrl + "/api/Photo/Backup";
        url_ = url_.replace(/[?&]$/, "");
        var content_ = "";
        var options_ = {
            body: content_,
            method: "get",
            headers: new __WEBPACK_IMPORTED_MODULE_9__angular_http__["a" /* Headers */]({
                "Content-Type": "application/json; charset=UTF-8",
                "Accept": "application/json; charset=UTF-8"
            })
        };
        return this.http.request(url_, options_).flatMap(function (response_) {
            return _this.processBackup(response_);
        }).catch(function (response_) {
            if (response_ instanceof __WEBPACK_IMPORTED_MODULE_9__angular_http__["e" /* Response */]) {
                try {
                    return _this.processBackup(response_);
                }
                catch (e) {
                    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(e);
                }
            }
            else
                return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(response_);
        });
    };
    PhotoManagementClient.prototype.processBackup = function (response) {
        var status = response.status;
        if (status === 200) {
            var responseText = response.text();
            var result200 = null;
            var resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            result200 = resultData200 !== undefined ? resultData200 : null;
            return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(result200);
        }
        else if (status !== 200 && status !== 204) {
            var responseText = response.text();
            return throwException("An unexpected server error occurred.", status, responseText);
        }
        return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
    };
    PhotoManagementClient.prototype.restore = function () {
        var _this = this;
        var url_ = this.baseUrl + "/api/Photo/Restore";
        url_ = url_.replace(/[?&]$/, "");
        var content_ = "";
        var options_ = {
            body: content_,
            method: "get",
            headers: new __WEBPACK_IMPORTED_MODULE_9__angular_http__["a" /* Headers */]({
                "Content-Type": "application/json; charset=UTF-8",
                "Accept": "application/json; charset=UTF-8"
            })
        };
        return this.http.request(url_, options_).flatMap(function (response_) {
            return _this.processRestore(response_);
        }).catch(function (response_) {
            if (response_ instanceof __WEBPACK_IMPORTED_MODULE_9__angular_http__["e" /* Response */]) {
                try {
                    return _this.processRestore(response_);
                }
                catch (e) {
                    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(e);
                }
            }
            else
                return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(response_);
        });
    };
    PhotoManagementClient.prototype.processRestore = function (response) {
        var status = response.status;
        if (status === 204) {
            var responseText = response.text();
            return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
        }
        else if (status !== 200 && status !== 204) {
            var responseText = response.text();
            return throwException("An unexpected server error occurred.", status, responseText);
        }
        return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
    };
    return PhotoManagementClient;
}());
PhotoManagementClient = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_8__angular_core__["b" /* Injectable */])(),
    __param(0, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_8__angular_core__["g" /* Inject */])(__WEBPACK_IMPORTED_MODULE_9__angular_http__["f" /* Http */])), __param(1, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_8__angular_core__["m" /* Optional */])()), __param(1, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_8__angular_core__["g" /* Inject */])(API_BASE_URL)),
    __metadata("design:paramtypes", [typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_9__angular_http__["f" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_9__angular_http__["f" /* Http */]) === "function" && _b || Object, String])
], PhotoManagementClient);

var MemberDto = (function () {
    function MemberDto(data) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    this[property] = data[property];
            }
        }
    }
    MemberDto.prototype.init = function (data) {
        if (data) {
            this.firstName = data["FirstName"];
            this.surname = data["Surname"];
            this.email = data["Email"];
            this.entryDate = data["EntryDate"] ? new Date(data["EntryDate"].toString()) : undefined;
            this.lastUpdate = data["LastUpdate"] ? new Date(data["LastUpdate"].toString()) : undefined;
            this.id = data["Id"];
            this.password = data["Password"];
        }
    };
    MemberDto.fromJS = function (data) {
        var result = new MemberDto();
        result.init(data);
        return result;
    };
    MemberDto.prototype.toJSON = function (data) {
        data = typeof data === 'object' ? data : {};
        data["FirstName"] = this.firstName;
        data["Surname"] = this.surname;
        data["Email"] = this.email;
        data["EntryDate"] = this.entryDate ? this.entryDate.toISOString() : undefined;
        data["LastUpdate"] = this.lastUpdate ? this.lastUpdate.toISOString() : undefined;
        data["Id"] = this.id;
        data["Password"] = this.password;
        return data;
    };
    return MemberDto;
}());

var ChangePasswordDto = (function () {
    function ChangePasswordDto(data) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    this[property] = data[property];
            }
        }
    }
    ChangePasswordDto.prototype.init = function (data) {
        if (data) {
            this.newPassword = data["NewPassword"];
            this.oldPassword = data["OldPassword"];
            this.email = data["Email"];
        }
    };
    ChangePasswordDto.fromJS = function (data) {
        var result = new ChangePasswordDto();
        result.init(data);
        return result;
    };
    ChangePasswordDto.prototype.toJSON = function (data) {
        data = typeof data === 'object' ? data : {};
        data["NewPassword"] = this.newPassword;
        data["OldPassword"] = this.oldPassword;
        data["Email"] = this.email;
        return data;
    };
    return ChangePasswordDto;
}());

var PhotoUploadData = (function () {
    function PhotoUploadData(data) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    this[property] = data[property];
            }
        }
    }
    PhotoUploadData.prototype.init = function (data) {
        if (data) {
            this.data = data["Data"];
            this.fileName = data["FileName"];
            this.title = data["Title"];
            this.email = data["Email"];
        }
    };
    PhotoUploadData.fromJS = function (data) {
        var result = new PhotoUploadData();
        result.init(data);
        return result;
    };
    PhotoUploadData.prototype.toJSON = function (data) {
        data = typeof data === 'object' ? data : {};
        data["Data"] = this.data;
        data["FileName"] = this.fileName;
        data["Title"] = this.title;
        data["Email"] = this.email;
        return data;
    };
    return PhotoUploadData;
}());

var PhotoManagementData = (function () {
    function PhotoManagementData(data) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    this[property] = data[property];
            }
        }
    }
    PhotoManagementData.prototype.init = function (data) {
        if (data) {
            this.fileName = data["FileName"];
            this.thumbnailBytes = data["ThumbnailBytes"];
            this.title = data["Title"];
            this.id = data["Id"];
            this.description = data["Description"];
        }
    };
    PhotoManagementData.fromJS = function (data) {
        var result = new PhotoManagementData();
        result.init(data);
        return result;
    };
    PhotoManagementData.prototype.toJSON = function (data) {
        data = typeof data === 'object' ? data : {};
        data["FileName"] = this.fileName;
        data["ThumbnailBytes"] = this.thumbnailBytes;
        data["Title"] = this.title;
        data["Id"] = this.id;
        data["Description"] = this.description;
        return data;
    };
    return PhotoManagementData;
}());

var PhotoMemberInfo = (function () {
    function PhotoMemberInfo(data) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    this[property] = data[property];
            }
        }
    }
    PhotoMemberInfo.prototype.init = function (data) {
        if (data) {
            this.email = data["Email"];
            this.fileName = data["FileName"];
            this.title = data["Title"];
            this.photoId = data["PhotoId"];
        }
    };
    PhotoMemberInfo.fromJS = function (data) {
        var result = new PhotoMemberInfo();
        result.init(data);
        return result;
    };
    PhotoMemberInfo.prototype.toJSON = function (data) {
        data = typeof data === 'object' ? data : {};
        data["Email"] = this.email;
        data["FileName"] = this.fileName;
        data["Title"] = this.title;
        data["PhotoId"] = this.photoId;
        return data;
    };
    return PhotoMemberInfo;
}());

var CommentData = (function () {
    function CommentData(data) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    this[property] = data[property];
            }
        }
    }
    CommentData.prototype.init = function (data) {
        if (data) {
            this.comment = data["Comment"];
            this.authorId = data["AuthorId"];
            this.photoId = data["PhotoId"];
            this.commentDate = data["CommentDate"] ? new Date(data["CommentDate"].toString()) : undefined;
            this.id = data["Id"];
        }
    };
    CommentData.fromJS = function (data) {
        var result = new CommentData();
        result.init(data);
        return result;
    };
    CommentData.prototype.toJSON = function (data) {
        data = typeof data === 'object' ? data : {};
        data["Comment"] = this.comment;
        data["AuthorId"] = this.authorId;
        data["PhotoId"] = this.photoId;
        data["CommentDate"] = this.commentDate ? this.commentDate.toISOString() : undefined;
        data["Id"] = this.id;
        return data;
    };
    return CommentData;
}());

var CommentUploadData = (function () {
    function CommentUploadData(data) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    this[property] = data[property];
            }
        }
    }
    CommentUploadData.prototype.init = function (data) {
        if (data) {
            this.comment = data["Comment"];
            this.email = data["Email"];
            this.photoId = data["PhotoId"];
            this.createDate = data["CreateDate"] ? new Date(data["CreateDate"].toString()) : undefined;
        }
    };
    CommentUploadData.fromJS = function (data) {
        var result = new CommentUploadData();
        result.init(data);
        return result;
    };
    CommentUploadData.prototype.toJSON = function (data) {
        data = typeof data === 'object' ? data : {};
        data["Comment"] = this.comment;
        data["Email"] = this.email;
        data["PhotoId"] = this.photoId;
        data["CreateDate"] = this.createDate ? this.createDate.toISOString() : undefined;
        return data;
    };
    return CommentUploadData;
}());

var SwaggerException = (function (_super) {
    __extends(SwaggerException, _super);
    function SwaggerException(message, status, response, result) {
        var _this = _super.call(this) || this;
        _this.message = message;
        _this.status = status;
        _this.response = response;
        _this.result = result;
        return _this;
    }
    return SwaggerException;
}(Error));

function throwException(message, status, response, result) {
    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(new SwaggerException(message, status, response, result));
}
function blobToText(blob) {
    return new __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"](function (observer) {
        var reader = new FileReader();
        reader.onload = function () {
            observer.next(this.result);
            observer.complete();
        };
        reader.readAsText(blob);
    });
}
var _a, _b;
//# sourceMappingURL=C:/Projekte/tmbit/photoaward/PhotoAward/PhotoAward.AngularClient/src/Controllers.generated.js.map

/***/ }),

/***/ 323:
/***/ (function(module, exports) {

module.exports = "<div class=\"jumbatron titlebar\">\r\n  <div class=\"row\">\r\n     <div class=\"col-xs-10\">\r\n       <h1>Microservices für .Net Entwickler:<br>{{title}}</h1>\r\n     </div>\r\n     <div class=\"col-xs-2 bitlogo\">\r\n       <a href=\"http://www.bridging-it.de/\" class=\"pull-right\">\r\n        <img src=\"assets/bitlogo.png\" width=\"200px\"/>\r\n      </a>\r\n     </div>\r\n   </div>\r\n</div>\r\n\r\n<div id='menubar' >\r\n<ul class=\"nav nav-pills\">\r\n  <li role=\"presentation\" ><a [routerLink]=\"['/gallery']\" *ngIf=\"userservice.user.notMember == false\">Gallerie</a></li>\r\n  <li role=\"presentation\" ><a [routerLink]=\"['/upload']\" *ngIf=\"userservice.user.notMember == false\">Bild hochladen</a></li>\r\n  <li role=\"presentation\"><a [routerLink]=\"['/']\" *ngIf=\"userservice.user.notMember == true\">Login</a></li>\r\n  <li role=\"presentation\" (click)='userservice.logout()'><a (click)='userservice.logout()' *ngIf=\"userservice.user.notMember == false\">Logout</a></li>\r\n  <li role=\"presentation\"><a [routerLink]=\"['/registerMember']\">Registrieren</a></li>\r\n</ul>\r\n</div>\r\n\r\n<div *ngIf=\"userservice.user.notMember == false\">\r\n  Willkommen {{userservice.user.firstname}} {{userservice.user.surname }}\r\n</div>\r\n\r\n<router-outlet></router-outlet>\r\n\r\n<div [hidden] = 'notMember'>\r\n\r\n<h3>Willkommen\r\n  {{firstname}}\r\n  {{surname}} </h3>\r\n\r\n  <div class=\"ui divider\">\r\n        Upload\r\n  </div>\r\n\r\n\r\n\r\n</div>\r\n\r\n<footer class=\"navbar navbar-fixed-bottom bitfooter\"><a href=\"http://www.bridging-it.de/content/impressum.html\">Impressum</a></footer>\r\n"

/***/ }),

/***/ 324:
/***/ (function(module, exports) {

module.exports = "<div class=\"panel panel-default\">\n  <h2>Anmelden</h2>\n  <form #form=\"ngForm\" (ngSubmit)=\"login(form.value)\">\n\n    <div class=\"form-group\">\n      <label for=\"email\" class=\"control-label col-xs-1\">Email</label>\n      <div class=\"col-xs-3\"><input type=\"email\" name=\"email\" ngModel [(ngModel)]=\"email\" class=\"form-control\"></div>\n    </div>\n    <div class=\"form-group\">\n      <label for=\"password\" class=\"control-label col-xs-1\">Passwort</label>\n      <div class=\"col-xs-3\"><input type=\"password\" name=\"password\" ngModel [(ngModel)]=\"password\" class=\"form-control\"></div>\n    </div>\n\n    <button type=\"submit\" class=\"btn btn-default\">Anmelden</button>\n    <br />\n    <p>&nbsp;</p>\n    <span>{{message}}</span>\n  </form>\n  <a [routerLink]=\"['/registerMember']\">Als neuer Benutzer registrieren</a>\n</div>\n"

/***/ }),

/***/ 325:
/***/ (function(module, exports) {

module.exports = "<div class=\"panel panel-default\">\r\n  <h2>Registrierung</h2>\r\n  <form #form=\"ngForm\" (ngSubmit)=\"register(form.value)\" class=\"form-horizontal\">\r\n\r\n    <div class=\"form-group\">\r\n      <label for=\"email\" class=\"control-label col-xs-1\">Email</label>\r\n      <div class=\"col-xs-3\"><input type=\"email\" name=\"email\" ngModel [(ngModel)]=\"email\" class=\"form-control\"></div>\r\n    </div>\r\n    <div class=\"form-group\">\r\n      <label for=\"password\" class=\"control-label col-xs-1\">Passwort</label>\r\n      <div class=\"col-xs-3\"><input type=\"password\" name=\"password\" ngModel [(ngModel)]=\"password\" class=\"form-control\"></div>\r\n    </div>\r\n    <div class=\"form-group\">\r\n      <label for=\"password2\" class=\"control-label col-xs-1\">Wiederholung</label>\r\n      <div class=\"col-xs-3\"><input type=\"password\" name=\"password2\" ngModel [(ngModel)]=\"password2\" class=\"form-control\"></div>\r\n    </div>\r\n    <div class=\"form-group\">\r\n      <label for=\"firstname\" class=\"control-label col-xs-1\">Vorname</label>\r\n      <div class=\"col-xs-3\">\r\n        <input type=\"text\" name=\"firstname\" ngModel [(ngModel)]=\"firstname\" class=\"form-control\">\r\n      </div>\r\n    </div>\r\n    <div class=\"form-group\">\r\n      <label for=\"surname\" class=\"control-label col-xs-1\">Nachname</label>\r\n      <div class=\"col-xs-3\">\r\n        <input type=\"text\" name=\"surname\" ngModel [(ngModel)]=\"surname\" class=\"form-control\">\r\n      </div>\r\n    </div>\r\n\r\n    <div class=\"form-group\">\r\n      <div class=\"col-xs-4\">\r\n        <span>{{message}}</span>\r\n      </div>\r\n    </div>\r\n    <div class=\"form-group\">\r\n      <div class=\"col-xs-4\">\r\n        <button type=\"submit\" class=\"btn btn-default\">Registrieren</button> </div>\r\n    </div>\r\n  </form>\r\n</div>\r\n<a [routerLink]=\"['/']\">Zurück zum Login</a>\r\n"

/***/ }),

/***/ 326:
/***/ (function(module, exports) {

module.exports = "<div class=\"panel panel-default\">\r\n  <h2>Benutzer auswählen</h2>\r\n  <form #form=\"ngForm\" (ngSubmit)=\"loadThumbnails()\" class=\"form-horizontal\">\r\n    <div class=\"form-group\">\r\n      <label for=\"email\" class=\"control-label col-xs-1\">Email</label>\r\n      <div class=\"col-xs-3\">\r\n      <input type=\"text\" name=\"email\" ngModel [(ngModel)]=\"email\" class=\"form-control\">\r\n      </div>\r\n    </div>\r\n    <div class=\"form-group\">\r\n      <label class=\"control-label col-xs-1\">&nbsp;</label>\r\n      <div class=\"col-xs-3\">\r\n      <button type=\"button\" class=\"btn btn-default\" (click)=\"loadThumbnails()\">Bilder anzeigen  </button>\r\n      </div>\r\n    </div>\r\n  </form>\r\n</div>\r\n\r\n<div class=\"panel panel-default\">\r\n  <div [hidden]='photos.length == 0'>\r\n    <div>Anzahl Bilder von {{email}} : {{photos.length}}</div>\r\n\r\n    <div class=\"row\">\r\n      <div class=\"col-sm-4 col-md-3\" *ngFor=\"let photo of photos\">\r\n        <div class=\"thumbnail\" >\r\n          <div class=\"caption\">\r\n            <h4>{{photo.title}}</h4>\r\n            <img [src]=\"'data:image/jpeg;base64,'+photo.thumbnailBytes\" width=\"150px\" /> Filename: {{photo.fileName}}\r\n            <br><span><i>{{photo.description}}</i></span>\r\n          </div>\r\n        </div>\r\n      </div>\r\n    </div>\r\n\r\n\r\n  </div>\r\n"

/***/ }),

/***/ 327:
/***/ (function(module, exports) {

module.exports = "<div class=\"panel panel-default\">\r\n<form #form=\"ngForm\" class=\"form-horizontal\" >\r\n      <h3>Upload Image</h3>\r\n\r\n   <div class=\"form-group\">\r\n    <label for=\"filetitle\" class=\"control-label col-xs-1\">Titel</label>\r\n     <div class=\"col-xs-3\">\r\n      <input type=\"text\" name=\"filetitle\" ngModel [(ngModel)]=\"filetitle\" class=\"form-control\" >\r\n      </div>\r\n    </div>\r\n    <div class=\"form-group\">\r\n    <label  class=\"control-label col-xs-1\">Datei</label>\r\n     <div class=\"col-xs-3\">\r\n      <input type=\"file\" (change)=\"fileChangeEvent($event)\" placeholder=\"Bild auswählen...\"  class=\"form-control btn\"/>\r\n      </div>\r\n    </div>\r\n\r\n\r\n  <div class=\"form-group\">\r\n    <label  class=\"control-label col-xs-1\">&nbsp;</label>\r\n     <div class=\"col-xs-3\">\r\n      <button type=\"button\" (click)=\"upload()\" class=\"btn btn-default\">Bild hinzufügen</button>\r\n      </div>\r\n    </div>\r\n\r\n     <div class=\"form-group\">\r\n    <label  class=\"control-label col-xs-1\">&nbsp;</label>\r\n     <div class=\"col-xs-3\">\r\n{{message}}\r\n      </div>\r\n    </div>\r\n\r\n</form>\r\n</div>\r\n"

/***/ }),

/***/ 33:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_router__ = __webpack_require__(46);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_app_Shared_user_user__ = __webpack_require__(93);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__(6);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return UserService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var UserService = (function () {
    function UserService(router) {
        this.router = router;
        var user = new __WEBPACK_IMPORTED_MODULE_1_app_Shared_user_user__["a" /* User */]();
        user.notMember = true;
        this.user = user;
    }
    UserService.prototype.logout = function () {
        if (this.user == null) {
            this.user = new __WEBPACK_IMPORTED_MODULE_1_app_Shared_user_user__["a" /* User */]();
        }
        else {
            var user = new __WEBPACK_IMPORTED_MODULE_1_app_Shared_user_user__["a" /* User */]();
            user.notMember = true;
            this.user = user;
        }
        this.router.navigateByUrl('/');
    };
    return UserService;
}());
UserService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2__angular_core__["b" /* Injectable */])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_router__["b" /* Router */]) === "function" && _a || Object])
], UserService);

var _a;
//# sourceMappingURL=C:/Projekte/tmbit/photoaward/PhotoAward/PhotoAward.AngularClient/src/user.service.js.map

/***/ }),

/***/ 362:
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(158);


/***/ }),

/***/ 62:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__user_service__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__Controllers_generated__ = __webpack_require__(28);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__(6);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return UploadService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};



var UploadService = (function () {
    function UploadService(userService, baseUrl) {
        this.userService = userService;
        this._baseUrl = baseUrl ? baseUrl : "";
    }
    UploadService.prototype.uploadFile = function (email, title, filename, params, files) {
        var _this = this;
        return new Promise(function (resolve, reject) {
            var url = _this._baseUrl + '/api/photo/uploadPhoto';
            var formData = new FormData(), xhr = new XMLHttpRequest();
            for (var i = 0; i < files.length; i++) {
                formData.append("uploads[]", files[i], files[i].name);
            }
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 1) {
                    if (_this.userService.token) {
                        xhr.setRequestHeader('Authorization', 'Bearer ' + _this.userService.token.access_token);
                    }
                    xhr.setRequestHeader("email", email);
                    xhr.setRequestHeader("title", title);
                    xhr.setRequestHeader("filename", filename);
                }
                if (xhr.readyState === 4) {
                    if (xhr.status === 200) {
                        resolve(xhr.responseText); //OK
                    }
                    else {
                        reject(xhr.statusText); //Error
                    }
                }
            };
            xhr.upload.onprogress = function (event) {
            };
            xhr.open('POST', url, true);
            var serverFileName = xhr.send(formData);
            return serverFileName;
        }); //Promise
    };
    return UploadService;
}());
UploadService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2__angular_core__["b" /* Injectable */])(),
    __param(1, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2__angular_core__["m" /* Optional */])()), __param(1, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2__angular_core__["g" /* Inject */])(__WEBPACK_IMPORTED_MODULE_1__Controllers_generated__["c" /* API_BASE_URL */])),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__user_service__["a" /* UserService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__user_service__["a" /* UserService */]) === "function" && _a || Object, String])
], UploadService);

var _a;
//# sourceMappingURL=C:/Projekte/tmbit/photoaward/PhotoAward/PhotoAward.AngularClient/src/uploadService.js.map

/***/ }),

/***/ 92:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_rxjs_add_observable_fromPromise__ = __webpack_require__(138);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_rxjs_add_observable_fromPromise___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_0_rxjs_add_observable_fromPromise__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_add_observable_of__ = __webpack_require__(139);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_add_observable_of___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_rxjs_add_observable_of__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_rxjs_add_observable_throw__ = __webpack_require__(140);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_rxjs_add_observable_throw___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_rxjs_add_observable_throw__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_rxjs_add_operator_map__ = __webpack_require__(142);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_rxjs_add_operator_map___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_3_rxjs_add_operator_map__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_rxjs_add_operator_toPromise__ = __webpack_require__(144);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_rxjs_add_operator_toPromise___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_4_rxjs_add_operator_toPromise__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5_rxjs_add_operator_mergeMap__ = __webpack_require__(143);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5_rxjs_add_operator_mergeMap___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_5_rxjs_add_operator_mergeMap__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6_rxjs_add_operator_catch__ = __webpack_require__(141);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6_rxjs_add_operator_catch___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_6_rxjs_add_operator_catch__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__ = __webpack_require__(3);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__angular_core__ = __webpack_require__(6);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__angular_http__ = __webpack_require__(61);
/* unused harmony export BearerToken */
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "b", function() { return API_BASE_URL; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return TokenService; });
/* unused harmony export SwaggerException */
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};










var BearerToken = (function () {
    function BearerToken() {
    }
    BearerToken.fromJS = function (data) {
        var result = new BearerToken();
        result.init(data);
        return result;
    };
    BearerToken.prototype.init = function (data) {
        if (data) {
            this.access_token = data["access_token"];
            this.token_type = data["token_type"];
            this.expires_in = data["expires_in"];
        }
    };
    return BearerToken;
}());

var API_BASE_URL = new __WEBPACK_IMPORTED_MODULE_8__angular_core__["Y" /* OpaqueToken */]('API_BASE_URL');
var TokenService = (function () {
    function TokenService(http, baseUrl) {
        this.jsonParseReviver = undefined;
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }
    TokenService.prototype.login = function (email, password) {
        var _this = this;
        var url_ = this.baseUrl + "/token";
        if (email === undefined) {
            throw new Error("The parameter 'email' must be defined.");
        }
        if (password === undefined) {
            throw new Error("The parameter 'password' must be defined.");
        }
        var content_ = "username=" + email + "&password=" + password + "&grant_type=password";
        var options_ = {
            body: content_,
            method: "post",
            headers: new __WEBPACK_IMPORTED_MODULE_9__angular_http__["a" /* Headers */]({
                "Content-Type": "application/x-www-form-urlencoded",
                "Accept": "application/json; charset=UTF-8"
            })
        };
        return this.http.request(url_, options_).flatMap(function (response_) {
            return _this.processLogin(response_);
        }).catch(function (response_) {
            if (response_ instanceof __WEBPACK_IMPORTED_MODULE_9__angular_http__["e" /* Response */]) {
                try {
                    return _this.processLogin(response_);
                }
                catch (e) {
                    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(e);
                }
            }
            else {
                return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(response_);
            }
        });
    };
    TokenService.prototype.processLogin = function (response) {
        var status = response.status;
        if (status === 200) {
            var responseText = response.text();
            var result200 = null;
            var resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
            result200 = resultData200 ? BearerToken.fromJS(resultData200) : null;
            return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(result200);
        }
        else if (status !== 200 && status !== 204) {
            var responseText = response.text();
            return throwException("An unexpected server error occurred.", status, responseText);
        }
        return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].of(null);
    };
    return TokenService;
}());
TokenService = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_8__angular_core__["b" /* Injectable */])(),
    __param(0, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_8__angular_core__["g" /* Inject */])(__WEBPACK_IMPORTED_MODULE_9__angular_http__["f" /* Http */])), __param(1, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_8__angular_core__["m" /* Optional */])()), __param(1, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_8__angular_core__["g" /* Inject */])(API_BASE_URL)),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_9__angular_http__["f" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_9__angular_http__["f" /* Http */]) === "function" && _a || Object, String])
], TokenService);

function throwException(message, status, response, result) {
    return __WEBPACK_IMPORTED_MODULE_7_rxjs_Observable__["Observable"].throw(new SwaggerException(message, status, response, result));
}
var SwaggerException = (function (_super) {
    __extends(SwaggerException, _super);
    function SwaggerException(message, status, response, result) {
        var _this = _super.call(this) || this;
        _this.message = message;
        _this.status = status;
        _this.response = response;
        _this.result = result;
        return _this;
    }
    return SwaggerException;
}(Error));

var _a;
//# sourceMappingURL=C:/Projekte/tmbit/photoaward/PhotoAward/PhotoAward.AngularClient/src/tokenservice.js.map

/***/ }),

/***/ 93:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return User; });
var User = (function () {
    function User() {
        this.notMember = false;
    }
    return User;
}());

var user = new User();
user.id = "";
//# sourceMappingURL=C:/Projekte/tmbit/photoaward/PhotoAward/PhotoAward.AngularClient/src/user.js.map

/***/ }),

/***/ 94:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__Shared_tokenservice__ = __webpack_require__(92);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_app_Shared_user_service__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__(6);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__Shared_Controllers_generated__ = __webpack_require__(28);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_app_Shared_user_user__ = __webpack_require__(93);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return LoginComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t;
    return { next: verb(0), "throw": verb(1), "return": verb(2) };
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = y[op[0] & 2 ? "return" : op[0] ? "throw" : "next"]) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [0, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};





var LoginComponent = (function () {
    function LoginComponent(_memberManagementClient, _userService, tokenService) {
        this._memberManagementClient = _memberManagementClient;
        this._userService = _userService;
        this.tokenService = tokenService;
    }
    LoginComponent.prototype.ngOnInit = function () {
    };
    LoginComponent.prototype.login = function (value) {
        return __awaiter(this, void 0, void 0, function () {
            var _this = this;
            var that;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.message = "Anmeldung gestartet ....";
                        that = this;
                        return [4 /*yield*/, this.tokenService.login(this.email, this.password).subscribe(function (token) {
                                _this._userService.token = token;
                                var dto = that._memberManagementClient.get(_this.email).subscribe(function (result) {
                                    that.user = new __WEBPACK_IMPORTED_MODULE_4_app_Shared_user_user__["a" /* User */]();
                                    that.user.firstname = result.firstName ? result.firstName : "";
                                    that.user.surname = result.surname ? result.surname : "";
                                    that.user.id = result.id;
                                    that.user.notMember = false;
                                    that.user.email = result.email ? result.email : "";
                                    that.message = "Willkommen " + that.user.firstname + " " + that.user.surname;
                                    _this._userService.user = that.user;
                                }, function (error) {
                                    that.user = new __WEBPACK_IMPORTED_MODULE_4_app_Shared_user_user__["a" /* User */]();
                                    that.user.notMember = true;
                                    that._userService.user = that.user;
                                    that.message = error.message;
                                    console.log(error);
                                });
                            })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/];
                }
            });
        });
    };
    return LoginComponent;
}());
LoginComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2__angular_core__["_14" /* Component */])({
        selector: 'pac-login',
        template: __webpack_require__(324),
        providers: [],
        styles: [],
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_3__Shared_Controllers_generated__["b" /* MemberManagementClient */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__Shared_Controllers_generated__["b" /* MemberManagementClient */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_1_app_Shared_user_service__["a" /* UserService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1_app_Shared_user_service__["a" /* UserService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_0__Shared_tokenservice__["a" /* TokenService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__Shared_tokenservice__["a" /* TokenService */]) === "function" && _c || Object])
], LoginComponent);

var _a, _b, _c;
//# sourceMappingURL=C:/Projekte/tmbit/photoaward/PhotoAward/PhotoAward.AngularClient/src/login.component.js.map

/***/ }),

/***/ 95:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__Shared_Controllers_generated__ = __webpack_require__(28);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__(6);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return RegisterMemberComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t;
    return { next: verb(0), "throw": verb(1), "return": verb(2) };
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = y[op[0] & 2 ? "return" : op[0] ? "throw" : "next"]) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [0, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};


var RegisterMemberComponent = (function () {
    function RegisterMemberComponent(_memberManagementClient) {
        this._memberManagementClient = _memberManagementClient;
    }
    RegisterMemberComponent.prototype.ngOnInit = function () {
        this.email = "tobias.meier@bridging-it.de";
    };
    RegisterMemberComponent.prototype.register = function (value) {
        return __awaiter(this, void 0, void 0, function () {
            var that, dto, dtoResult;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        if (this.password !== this.password2) {
                            this.message = "Passwörter sind unterschiedlich";
                            return [2 /*return*/];
                        }
                        this.message = "Die Registrierung wird übermittelt ...";
                        that = this;
                        dto = new __WEBPACK_IMPORTED_MODULE_0__Shared_Controllers_generated__["d" /* MemberDto */]();
                        dto.firstName = this.firstname;
                        dto.surname = this.surname;
                        dto.email = this.email;
                        dto.password = this.password;
                        return [4 /*yield*/, this._memberManagementClient.add(dto).subscribe(function (result) {
                                that.firstname = result.firstName ? result.firstName : "";
                                that.surname = result.surname ? result.surname : "";
                                that.id = result.id;
                                that.message = "Die Registrierung war erfolgreich. Bitte melden Sie sich nun an.";
                            }, function (e) {
                                that.message = "Fehler: " + e.message;
                            })];
                    case 1:
                        dtoResult = _a.sent();
                        return [2 /*return*/];
                }
            });
        });
    };
    return RegisterMemberComponent;
}());
RegisterMemberComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["_14" /* Component */])({
        selector: 'pac-register-member',
        template: __webpack_require__(325),
        styles: []
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__Shared_Controllers_generated__["b" /* MemberManagementClient */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__Shared_Controllers_generated__["b" /* MemberManagementClient */]) === "function" && _a || Object])
], RegisterMemberComponent);

var _a;
//# sourceMappingURL=C:/Projekte/tmbit/photoaward/PhotoAward/PhotoAward.AngularClient/src/register-member.component.js.map

/***/ }),

/***/ 96:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__Shared_user_service__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__Shared_Controllers_generated__ = __webpack_require__(28);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__(6);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_router___ = __webpack_require__(46);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ShowPhotosComponentComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var ShowPhotosComponentComponent = (function () {
    function ShowPhotosComponentComponent(_photoManagementClient, _userService, router) {
        this._photoManagementClient = _photoManagementClient;
        this._userService = _userService;
        this.router = router;
        this.photos = [];
    }
    ShowPhotosComponentComponent.prototype.ngOnInit = function () {
        if (this._userService && this._userService.user && this._userService.user.notMember === false) {
            this.email = this._userService.user.email;
            if (this.email) {
                this.loadThumbnails();
                return;
            }
        }
        this.router.navigateByUrl('/');
    };
    ShowPhotosComponentComponent.prototype.loadThumbnails = function () {
        this.showImagesOfMember(this.email);
    };
    ShowPhotosComponentComponent.prototype.showImagesOfMember = function (email) {
        var that = this;
        console.log("Loading images for user " + email);
        this._photoManagementClient.getThumbnailsOfMember(email).subscribe(function (images) {
            /*console.log("Bilder ermittelt: ");
            if (images == null) {
              console.log("Keine bilder!");
            } else {
              console.log("Bilder Anzahl: " + images.length);
            }*/
            that.photos = images != null ? images : [];
        }, function (error) {
            console.log(error);
        });
    };
    return ShowPhotosComponentComponent;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2__angular_core__["M" /* Input */])(),
    __metadata("design:type", String)
], ShowPhotosComponentComponent.prototype, "email", void 0);
ShowPhotosComponentComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2__angular_core__["_14" /* Component */])({
        selector: 'pac-show-photos-component',
        template: __webpack_require__(326),
        styles: []
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__Shared_Controllers_generated__["a" /* PhotoManagementClient */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__Shared_Controllers_generated__["a" /* PhotoManagementClient */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_0__Shared_user_service__["a" /* UserService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__Shared_user_service__["a" /* UserService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_3__angular_router___["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__angular_router___["b" /* Router */]) === "function" && _c || Object])
], ShowPhotosComponentComponent);

var _a, _b, _c;
//# sourceMappingURL=C:/Projekte/tmbit/photoaward/PhotoAward/PhotoAward.AngularClient/src/show-photos-component.component.js.map

/***/ }),

/***/ 97:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_app_Shared_user_service__ = __webpack_require__(33);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__Shared_uploadService__ = __webpack_require__(62);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__(6);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_router___ = __webpack_require__(46);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return UploadPhotoComponentComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t;
    return { next: verb(0), "throw": verb(1), "return": verb(2) };
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = y[op[0] & 2 ? "return" : op[0] ? "throw" : "next"]) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [0, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};




var UploadPhotoComponentComponent = (function () {
    function UploadPhotoComponentComponent(_uploadService, _userService, router) {
        this._uploadService = _uploadService;
        this._userService = _userService;
        this.router = router;
        this.imageUploaded = new __WEBPACK_IMPORTED_MODULE_2__angular_core__["D" /* EventEmitter */]();
        this.message = "";
    }
    UploadPhotoComponentComponent.prototype.ngOnInit = function () {
        if (this._userService && this._userService.user && this._userService.user.notMember === false) {
            return;
        }
        this.router.navigateByUrl('/');
    };
    UploadPhotoComponentComponent.prototype.fileChangeEvent = function (fileInput) {
        this._filesToUpload = fileInput.target.files;
    };
    UploadPhotoComponentComponent.prototype.upload = function () {
        return __awaiter(this, void 0, void 0, function () {
            var that_1, files, filename, result;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        if (!(this._filesToUpload.length > 0)) return [3 /*break*/, 2];
                        that_1 = this;
                        files = this._filesToUpload;
                        filename = files[0].name;
                        that_1.message = "Uploading image " + filename;
                        return [4 /*yield*/, this._uploadService.uploadFile(this._userService.user.email, this.filetitle, filename, [], files).then(function (e) {
                                that_1.message = "Bild wurde hochgeladen";
                                that_1.imageUploaded.emit("");
                                that_1.filetitle = "";
                                that_1._filesToUpload = [];
                            }).catch((function (error) {
                                that_1.message = "Upload fehlgeschlagen: " + error;
                            }))];
                    case 1:
                        result = _a.sent();
                        //ToDo: ShowImages
                        //await this.showImagesOfMember(this.email);
                        console.log("Upload beendet!");
                        _a.label = 2;
                    case 2: return [2 /*return*/];
                }
            });
        });
    };
    return UploadPhotoComponentComponent;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2__angular_core__["_4" /* Output */])(),
    __metadata("design:type", Object)
], UploadPhotoComponentComponent.prototype, "imageUploaded", void 0);
UploadPhotoComponentComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2__angular_core__["_14" /* Component */])({
        selector: 'pac-upload-photo-component',
        template: __webpack_require__(327),
        styles: []
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__Shared_uploadService__["a" /* UploadService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__Shared_uploadService__["a" /* UploadService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_0_app_Shared_user_service__["a" /* UserService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0_app_Shared_user_service__["a" /* UserService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_3__angular_router___["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__angular_router___["b" /* Router */]) === "function" && _c || Object])
], UploadPhotoComponentComponent);

var _a, _b, _c;
//# sourceMappingURL=C:/Projekte/tmbit/photoaward/PhotoAward/PhotoAward.AngularClient/src/upload-photo-component.component.js.map

/***/ })

},[362]);
//# sourceMappingURL=main.bundle.js.map