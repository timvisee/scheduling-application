/**
 * Event API.
 */
export default (api) => {
    /**
     * Constructor.
     */
    var event = {};

    /**
    * Fetch a list of all events.
    *
    * @param {int} id Event ID.
    * @return Promise.
    */
    event.fetchAll = function() {
        return api.ajaxGet("event");
    };

    /**
    * Fetch properties of a specific event.
    *
    * @param {int} id Event ID.
    * @return Promise.
    */
    event.fetch = function(id) {
        return api.ajaxGet("event/details/" + id);
    };

    return event;
};
