export function dynamicSearch(selector, apiURL, maxNumOfResults, submitALlowed, renderItem, clickItem){
    var targetEl = (selector[0] === "#") ? $(selector) : $(selector).first();
    if(submitALlowed === false){
        targetEl.keypress(function(key){
            if(key.which == 13) key.preventDefault();
        });
    }
    targetEl.keyup(function(){
        var inputValue = targetEl.val()
        //clear previous result
        if(targetEl.next().hasClass("dynamic-search-result"))
            targetEl.next().remove();
        //generate new
        if(inputValue != ""){
            dynamicSearchRequest(targetEl, apiURL, maxNumOfResults, inputValue, renderItem, clickItem);
        }
    })
}

function dynamicSearchRequest(targetEl, apiURL, maxNumOfResults, inputValue, renderItem, clickItem){
    var request = `${apiURL}?searchStr=${inputValue}&maxNumOfResults=${maxNumOfResults}`;
    $.ajax({
        type: 'GET',
        url: request,
        success: function(responceJson) {
            var responce = JSON.parse(responceJson);
            renderResult(responce, targetEl, renderItem, clickItem);  
        }
    });
}

function renderResult(result, targetEl, renderItem, clickItem){
    var promptListHtml = `<ul class="list-group dynamic-search-result">`;
    var idList = []
    result.forEach(element => {
        var elementId = element.Id != undefined? `option-${element.Id}`: `option-${element.Name}`;
        idList.push(elementId)
        promptListHtml += `<li class="dynamic-search-result-item list-group-item" id="${elementId}">${renderItem(element, elementId)}</li>`;
    });
    promptListHtml += "</ul>"
    targetEl.after(promptListHtml);

    if(clickItem != undefined){
        for (var i = 0; i<result.length; i++){
            $('#'+idList[i]).click({tag_title: result[i].Name, tag_id: idList[i].replace('option-', '')}, 
                                    clickItem)
        }
    }
}