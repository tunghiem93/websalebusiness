/*1522223386,,JIT Construction: v3761851,en_US*/

/**
 * Copyright (c) 2017-present, Facebook, Inc. All rights reserved.
 *
 * You are hereby granted a non-exclusive, worldwide, royalty-free license to use,
 * copy, modify, and distribute this software in source code or binary form for use
 * in connection with the web services and APIs provided by Facebook.
 *
 * As with any software that integrates with the Facebook platform, your use of
 * this software is subject to the Facebook Platform Policy
 * [http://developers.facebook.com/policy/]. This copyright notice shall be
 * included in all copies or substantial portions of the software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
(function _(a,b){var c=24*60*60,d=7*c,e="https://developers.facebook.com/docs/accountkit/integratingweb#configureloginhtml",f="Please ensure the AccountKit SDK is hotlinked directly. See "+e,g=Math.floor(new Date().getTime()/1e3)-b;if(g>d)throw new Error("The SDK is more than 7 days old. "+f);else if(g>c){var h=window.console;if(h)h.warn("The SDK is more than 1 day old. "+f)}if(!window.AccountKit)window.AccountKit={doNotLinkToSDKDirectly:"doNotLinkToSDKDirectly"};var i=document.createElement("script");i.src=a;i.async=true;var j=document.getElementsByTagName("script")[0];j.parentNode&&j.parentNode.insertBefore(i,j)})("https:\/\/sdk.accountkit.com\/en_US\/sdk.js?hash=7676da4011c35b1f35f75d10a0472b22", 1522223386);