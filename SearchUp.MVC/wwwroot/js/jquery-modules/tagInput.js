export function tagInput(selector, preselectedTags){
    var tagInputObj = (selector[0] === "#") ? $(selector) : $(selector).first();
    preselectedTags = preselectedTags === undefined? []: preselectedTags;
    preselectedTags.forEach(tag => {pushTag(tagInputObj, tag, true)});
}

export function pushTag(tagInputObj, tagTitle, tagId, isPreselected){
    var htmlTagId = getTagId(tagInputObj, tagTitle);
    isPreselected = isPreselected===undefined? false: isPreselected;
    if(getAllTags(tagInputObj).map(t=>t.title).includes(tagTitle)){
        var tag = $(`#${htmlTagId}`);
        tag.attr('tagSelected', 'true');
        tag.show();
    }
    else {
        tagInputObj.append(`<span class="tag interest-tag" elid="${tagId}" id="${htmlTagId}" tagSelected="true" tagPreselected="${isPreselected}">
                                <span class="tag-title">${tagTitle}</span>
                                <span class="tag-remove" id="tag-remove-${htmlTagId}"> ×</span>
                            </span>`);
        $(`#tag-remove-${htmlTagId}`).click({tag_input: tagInputObj, tag_title: tagTitle} ,popTag);
    }
}

export function popTag(event){
    var tag_input = event.data.tag_input;
    var tag_title = event.data.tag_title;
    var tagId = getTagId(tag_input, tag_title);
    var tag = $('#'+tagId);
    if(getSelectedTags(tag_input).map(t=>t.title).includes(tag_title)){
        $(tag).attr('tagSelected', 'false');
        $(tag).hide();
    }
}

export function getAllTags(tagInput){
    var result = []; 
    $(`#${tagInput.prop('id')} > .tag`).each(function() {
        result.push({title: $(this).children('.tag-title').text(), id: parseInt($(this).attr('elid'))})
    });
    return result;
}

export function getSelectedTags(tagInput){
    var result = []; 
    var tags = $(`#${tagInput.prop('id')} > .tag`);
    $(`#${tagInput.prop('id')} > .tag`).each(function() {
        if($(this).attr('tagSelected') == 'true')
            result.push({title: $(this).children('.tag-title').text(), id: parseInt($(this).attr('elid'))})
    });
    return result;
}

export function getRemovedTags(tagInput){
    var result = []; 
    $(`#${tagInput.prop('id')} > .tag`).each(function() {
        if($(this).attr('tagSelected') == 'false' && this.attr('tagPreselected') == 'true')
            result.push({title: $(this).children('.tag-title').text(), id: parseInt($(this).attr('elid'))})
    });
    return result;
}

export function getAddedTags(tagInput){
    var result = []; 
    $(`#${tagInput.prop('id')} > .tag`).each(function() {
        if($(this).attr('tagSelected') == 'true' && $(this).attr('tagPreselected') == 'false')
            result.push({title: $(this).children('.tag-title').text(), id: parseInt($(this).attr('elid'))})
    });
    return result;
}

function getTagId(tagInput, tagTitle){
    return `${tagInput.attr('id')}-${tagTitle.replace(' ','').replace('.','dot')}`;
}
