/**
 * Base API client.
 */

import _ from 'lodash';
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
 * An options object, that is used to configure an Ajax request.
 *
 * @deftype {AjaxOptions}
 * @param {object|null} [progress=null] The context of the Vue component to
 *                                      render the progres bar for.
 * @parma {object|status} [status=null] The status object, to set the request
 *                                      status in. This is usually a field in
 *                                      a Vue component, such as `status`.
 */

/**
 * The default Ajax options object.
 *
 * @type {AjaxOptions}
 */
const AJAX_OPTIONS_DEF = {
    progress: null,
    status: null,
};

/**
 * Send a GET request through AJAX on the given endpoint.
 *
 * @param {string} endpoint Endpoint to call.
 * @param {} [options] Additional options for the ajax call.
 *
 * @return Promise for the response.
 */
api.ajaxGet = function(endpoint, options) {
    // Build the options object
    options = _.defaults(options, AJAX_OPTIONS_DEF);

    // Create a promise for the request, return it
    return new Promise(function(resolve, reject) {
        // Start the progress bar
        if(options.progress)
            options.progress.$Progress.start();

        // Update the status
        if(options.status) {
            options.status.loading = true;
            options.status.error = null;
            options.status.ok = false;
        }

        // Make the request
        axios.get(api.getUrl(endpoint))
            .then(response => {
                // Resolve the promise
                resolve(response.data);

                // Stop the progress bar
                if(options.progress)
                    options.progress.$Progress.finish();

                // Update the status
                if(options.status)
                    options.status.loading = false, options.status.ok = true;
            })
            .catch(error => {
                // Report the error, and continue
                console.error("AJAX API request error: " + error);

                // Reject the promise
                reject(error);

                // Fail the progress bar
                if(options.progress)
                    options.progress.$Progress.fail();

                // Update the status
                if(options.status)
                    options.status.loading = false, options.status.error = error;
            });
    });
};

// Add sub APIs
api.event = apiEvent(api);

// Export the whole API
export default api;
