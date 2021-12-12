import { tagInput, pushTag, getAddedTags, getRemovedTags } from "./tagInput.js";
import { dynamicSearch } from "./dynamicSearch.js";

// Function creates interest selecting widget that includes search bar (for searching interests)
// as tagInput widget where selected interests are included as tags
// PARAMS: selector, opts{
//                      preselectedValues, // preselected values for taginput
//                      apiURL,            // api url for dynamic search recourse 
//                      maxNumOfResults,   // maximum number of displayed results
//                      submitAllowed,     // if false, then it blocks onEnter submit
//                      renderItemFunc     // how dynamic search item will be rendered as html
//                   }
export function selectInterests(selector, opts){
    var selectObj = selector[0] === '#' ? $(selector) : $(selector).first();
    // add tag input component
    selector = selector.slice(1);
    selectObj.append(`<div id="${selector}-tag-input"></div>`);
    tagInput(`#${selector}-tag-input`, opts.preselectedValues);
    // add dynamic search component
    selectObj.append(`<input id="${selector}-dynamic-search" class="form-control mt-3 dynamic-search" type="text" placeholder="Search for existing topics and click them to add"></input>`);
    
    var clickSesrchItem = function(event, tagInputId=`#${selector}-tag-input`){
        pushTag($(tagInputId), event.data.tag_title, event.data.tag_id, false);
    }
    dynamicSearch(`#${selector}-dynamic-search`, opts.apiURL, opts.maxNumOfResults, opts.submitAllowed, opts.renderItem, clickSesrchItem);
}

export function getAddedInterests(selectInterestsId){
    const tagInputId = `${selectInterestsId}-tag-input`;
    return getAddedTags($('#'+tagInputId));
}

export function getRemovedInterests(selectInterestsId){
    const tagInputId = `${selectInterestsId}-tag-input`;
    return getRemovedTags($('#'+tagInputId));
}