/**
 * People API.
 */
export default (api) => {
    /**
     * Constructor.
     */
    var people = {};

    /**
    * Fetch a list of all people .
    *
    * @param {int} id People ID.
    * @return Promise.
    */
    people .fetchAll = function() {
        return api.ajaxGet("people/list");
    };

    /**
    * Fetch properties of a specific people.
    *
    * @param {int} id People ID.
    * @return Promise.
    */
    people.fetch = function(id) {
        return api.ajaxGet("people/details/" + id);
    };

    // Return the people API
    return people;
};
