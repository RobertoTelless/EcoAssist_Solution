/*
@license
dhtmlxScheduler.Net v.4.0.1 Professional Evaluation

This software is covered by DHTMLX Evaluation License. Contact sales@dhtmlx.com to get Commercial or Enterprise license. Usage without proper license is prohibited.

(c) Dinamenta, UAB.
*/
Scheduler.plugin(function(e){!function(){var t=e.dhtmlXTooltip=e.tooltip={};t.config={className:"dhtmlXTooltip tooltip",timeout_to_display:50,timeout_to_hide:50,delta_x:15,delta_y:-20},t.tooltip=document.createElement("div"),t.tooltip.className=t.config.className,e._waiAria.tooltipAttr(t.tooltip),t.show=function(a,i){if(!this._mobile||e.config.touch_tooltip){var n=t,r=this.tooltip,o=r.style;n.tooltip.className=n.config.className;var l=this.position(a),d=a.target||a.srcElement;if(!this.isTooltip(d)){
var s=l.x+(n.config.delta_x||0),_=l.y-(n.config.delta_y||0);o.visibility="hidden",o.removeAttribute?(o.removeAttribute("right"),o.removeAttribute("bottom")):(o.removeProperty("right"),o.removeProperty("bottom")),o.left="0",o.top="0",this.tooltip.innerHTML=i,document.body.appendChild(this.tooltip);var c=this.tooltip.offsetWidth,u=this.tooltip.offsetHeight;document.documentElement.clientWidth-s-c<0?(o.removeAttribute?o.removeAttribute("left"):o.removeProperty("left"),o.right=document.documentElement.clientWidth-s+2*(n.config.delta_x||0)+"px"):0>s?o.left=l.x+Math.abs(n.config.delta_x||0)+"px":o.left=s+"px",
document.documentElement.clientHeight-_-u<0?(o.removeAttribute?o.removeAttribute("top"):o.removeProperty("top"),o.bottom=document.documentElement.clientHeight-_-2*(n.config.delta_y||0)+"px"):0>_?o.top=l.y+Math.abs(n.config.delta_y||0)+"px":o.top=_+"px",e._waiAria.tooltipVisibleAttr(this.tooltip),o.visibility="visible",this.tooltip.onmouseleave=function(t){t=t||window.event;for(var a=e.dhtmlXTooltip,i=t.relatedTarget;i!=e._obj&&i;)i=i.parentNode;i!=e._obj&&a.delay(a.hide,a,[],a.config.timeout_to_hide);
},e.callEvent("onTooltipDisplayed",[this.tooltip,this.tooltip.event_id])}}},t._clearTimeout=function(){this.tooltip._timeout_id&&window.clearTimeout(this.tooltip._timeout_id)},t.hide=function(){if(this.tooltip.parentNode){e._waiAria.tooltipHiddenAttr(this.tooltip);var t=this.tooltip.event_id;this.tooltip.event_id=null,this.tooltip.onmouseleave=null,this.tooltip.parentNode.removeChild(this.tooltip),e.callEvent("onAfterTooltip",[t])}this._clearTimeout()},t.delay=function(e,t,a,i){this._clearTimeout(),
this.tooltip._timeout_id=setTimeout(function(){var i=e.apply(t,a);return e=t=a=null,i},i||this.config.timeout_to_display)},t.isTooltip=function(e){for(var t=!1;e&&!t;)t=e.className==this.tooltip.className,e=e.parentNode;return t},t.position=function(e){return e=e||window.event,{x:e.clientX,y:e.clientY}},e.attachEvent("onMouseMove",function(a,i){var n=window.event||i,r=n.target||n.srcElement,o=t,l=o.isTooltip(r),d=o.isTooltipTarget&&o.isTooltipTarget(r);if(a&&e.getState().editor_id!=a||l||d){var s;
if(a||o.tooltip.event_id){var _=e.getEvent(a)||e.getEvent(o.tooltip.event_id);if(!_)return;if(o.tooltip.event_id=_.id,s=e.templates.tooltip_text(_.start_date,_.end_date,_),!s)return o.hide()}d&&(s="");var c;if(_isIE){c={pageX:void 0,pageY:void 0,clientX:void 0,clientY:void 0,target:void 0,srcElement:void 0};for(var u in c)c[u]=n[u]}if(!e.callEvent("onBeforeTooltip",[a])||!s)return;o.delay(o.show,o,[c||n,s])}else o.delay(o.hide,o,[],o.config.timeout_to_hide)}),e.attachEvent("onBeforeDrag",function(){
return t.hide(),!0}),e.attachEvent("onEventDeleted",function(){return t.hide(),!0})}()});