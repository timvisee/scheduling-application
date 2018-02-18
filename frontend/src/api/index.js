/**
 * Base API client.
 */

import axios from 'axios';

import apiEvent from './event';

/**
 * API base object.
 */
var api = {
    /**
     * The host the application is hosted on.
     * TODO: fetch this from some global file
     */
    host: "http://localhost:5000/api/v1/",
};

/**
 * Get and determine the URL for an API endpoint.
 */
api.getUrl = function(endpoint) {
    return api.host + endpoint;
};

/**
 * Send a GET request through AJAX on the given endpoint.
 *
 * @param {string} Endpoint to call.
 * @return Promise.
 */
api.ajaxGet = function(endpoint) {
    // Create a promise for the request, return it
    return new Promise(function(resolve, reject) {
        // Make the request
        axios.get(api.getUrl(endpoint))
            .then(response => resolve(response.data))
            .catch(error => {
                // Report the error, and continue
                console.error("AJAX API request error: " + error);

                // Reject the promise
                reject(error);
            });
    });
};

// Add sub APIs
api.event = apiEvent(api);

// Export the whole API
export default api;
