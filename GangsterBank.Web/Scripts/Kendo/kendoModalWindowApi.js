//open kendo window with specified content and params
//required params - url or content, title; additional params - height, width, selector
function openKendoWindow(options) {
    var kendoModalWindowSelector = $(options.selector || "#kendoModalWindow");
    var kendoModalWindow = kendoModalWindowSelector.data("kendoWindow");
    kendoModalWindow.setOptions({ minHeight: 70 });
    if (options.height) kendoModalWindow.setOptions({ height: options.height });
    if (options.width) kendoModalWindow.setOptions({ width: options.width });
    if (options.closeHandler && typeof options.closeHandler == "function") {
        kendoModalWindowSelector.data("closeHandler", options.closeHandler);
        kendoModalWindow.bind("close", options.closeHandler);
    }
    if (options.refreshHandler) kendoModalWindowSelector.data("refreshHandler", options.refreshHandler);
    if (options.url) {
        kendoModalWindow.content('<div class = "k-loading-image"></div>');
        var data = options.data,
            newUrl = options.url;
        if (data) {
            newUrl += "?";
            $.each(data, function (i, item) {
                newUrl += i + "=" + item + "&";
            });
            newUrl = newUrl.substring(0, newUrl.length - 1);
            options.url = newUrl;
        }
        kendoModalWindow.refresh(options.url);
    }
    if (options.content) kendoModalWindow.content(options.content);
    if (options.title) kendoModalWindow.title(options.title);
    if (options.contentAction) options.contentAction(kendoModalWindowSelector);
    kendoModalWindow.open().center();
}

//close modal window
function closeKendoWindow(selector) {
    var kendoModalWindowSelector = selector || $('#kendoModalWindow');
    kendoModalWindowSelector.data('kendoWindow').close();
}

//Refresh Event handler for kendoModalWindow 
function onRefreshKendoWindow(e) {
    var kendoModalWindowSelector = e.sender.element || $('#kendoModalWindow');
    kendoModalWindowSelector.find(".k-window").height("");
    var handler = kendoModalWindowSelector.data("refreshHandler");
    if (typeof handler == "function") {
        handler(e);
    }
    this.center();
}

function centerKendoWindow(selector) {
    var kendoModalWindow = $(selector || '#kendoModalWindow').data("kendoWindow");
    kendoModalWindow.center();
}

//Close Event handler for kendoModalWindow 
function onCloseKendoModalWindow(selector) {
    var kendoModalWindowSelector = selector.sender.element || $('#kendoModalWindow');
    var handler = kendoModalWindowSelector.data("closeHandler");
    if (typeof handler == "function") {
        kendoModalWindowSelector.data("kendoWindow").unbind("close", handler);
    }
    //clear url and remove iframe before closing window. Prevents bugs in safari and chrome.
    kendoModalWindowSelector.find(".k-window iframe").attr("src", "").remove();
}

function resizeAndCenterModalWindow() {
    resizeAndCenterBaseEventHandler();
}

function resizeAndCenterBaseEventHandler(mwSelector) {
    var selector = mwSelector || modalWindowSelector;
    if (!selector.data("prevent-resize")) {
        $(".t-window-content", selector).css({ height: "" });
    }
    var pop = selector.data('tWindow');
    $("input[type='text']:first", selector).focus();
    if (pop) {
        pop.center();
    }
}