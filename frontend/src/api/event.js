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
    * @param {AjaxOptions} [options] Ajax options.
    * @return Promise.
    */
    event.fetchAll = function(options) {
        return api.ajaxGet("event/list", options);
    };

    /**
    * Fetch properties of a specific event.
    *
    * @param {int} id Event ID.
    * @param {AjaxOptions} [options] Ajax options.
    * @return Promise.
    */
    event.fetch = function(id, options) {
        return api.ajaxGet("event/details/" + id, options);
    };

    // Return the event API
    return event;
};
