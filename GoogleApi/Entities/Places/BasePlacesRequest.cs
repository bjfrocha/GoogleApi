﻿using System;

namespace GoogleApi.Entities.Places
{
    /// <summary>
    /// Base abstract class for Places requests.
    /// </summary>
    public abstract class BasePlacesRequest : BaseRequest
    {
        /// <summary>
        /// Base Url.
        /// </summary>
        protected internal override string BaseUrl => "maps.googleapis.com/maps/api/place/";

        /// <summary>
        /// True to indicate that request comes from a device with a location sensor, otherwise false. 
        /// This information is required by Google and does not affect the results.
        /// </summary>
        /// <remarks>
        /// It is unclear if Google refers to the device performing the request or the source of the location data.
        /// In the geocoding API reference at https://developers.google.com/maps/documentation/geocoding/ the definition is:
        /// 
        ///     sensor (required) — Indicates whether or not the geocoding request comes from a device with a location sensor.
        /// 
        /// This implies that only mobile devices that are equipped with a location sensor (such as GPS) should set the Sensor value 
        /// to True. So if a location is sent to a web server which then calls the Google API, it apparently should set the Sensor
        /// value to false, since the web server isn't a location sensor equipped device.
        /// 
        /// In another page of their documentation, https://developers.google.com/maps/documentation/javascript/tutorial they say:
        /// 
        ///     The sensor parameter of the URL must be included, and indicates whether this application uses a sensor (such as a GPS 
        ///     locator) to determine the user's location.
        /// 
        /// This implies something completely different, that the Sensor value must be set to true if the source of the location
        /// information is a sensor or not, regardless if the request is being performed by a location sensor equipped device or not.
        /// </remarks>
        public virtual bool Sensor { get; set; }

        /// <summary>
        /// Always true. Setter is not supported.
        /// </summary>
        public override bool IsSsl
        {
            get => true;
            set => throw new NotSupportedException("This operation is not supported, Request must use SSL");
        }

        /// <summary>
        /// See <see cref="BaseRequest.GetQueryStringParameters()"/>.
        /// </summary>
        /// <returns>The <see cref="QueryStringParameters"/> collection.</returns>
        public override QueryStringParameters GetQueryStringParameters()
        {
            if (string.IsNullOrWhiteSpace(this.Key))
                throw new ArgumentException("Key is required");

            var parameters = base.GetQueryStringParameters();

            parameters.Add("sensor", Sensor.ToString().ToLower());

            return parameters;
        }
    }
}