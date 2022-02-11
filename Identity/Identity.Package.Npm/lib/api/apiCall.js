"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
var _a;
Object.defineProperty(exports, "__esModule", { value: true });
exports.axios = exports.addHeader = void 0;
var axios_1 = __importDefault(require("axios"));
var _headers = (_a = {},
    _a["Content-Type"] = "application/json",
    _a["Connection"] = "Keep-alive",
    _a);
var _config = {
    baseURL: 'https://localhost:7130/api/',
    timeout: 60 * 1000,
    headers: _headers
};
/**
 * Add header to Axios Request Header
 * @param key Header Key for authentication add I-Authentication
 * @param value Header Value
 */
var addHeader = function (key, value) {
    _headers[key] = value;
};
exports.addHeader = addHeader;
exports.axios = axios_1.default.create(_config);
