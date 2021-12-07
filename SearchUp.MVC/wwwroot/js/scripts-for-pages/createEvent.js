import {getAddedInterests, selectInterests} from "../jquery-modules/selectInterests.js";
selectInterests('#select-interests', 
                {apiURL: 'https://localhost:5001/Interests/GetInterestsBySubstring', // api url for dynamic search recourse 
                maxNumOfResults: 7,                                                  // maximum number of displayed results
                submitAllowed: false,                                                // if false, then it blocks onEnter submit
                renderItem: function(topic)                                          // how dynamic search item will rendered as html
                {
                    return `<div>${topic.Name}</div>`}                             
                }
);
// insert selected interests into hidden input when submit
$("#create-event-form").submit(function() {
    $('#added-interests').val(getAddedInterests('select-interests'));
})