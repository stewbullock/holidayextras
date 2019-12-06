using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM.Core
{
    public static class Extension
    {
        /// <summary>
        /// Check for a failed response
        /// </summary>
        /// <param name="responses"></param>
        /// <returns></returns>
        public static ResponseState Failed(this List<IResponse> responses)
        {
            if (responses == null || responses.Count == 0) return ResponseState.Success;

            return responses.Any(response => response.ResponseState == ResponseState.Failed)
                       ? ResponseState.Failed
                       : ResponseState.Success;
        }

        /// <summary>
        /// Aggregate multiple response failures
        /// </summary>
        /// <param name="responses"></param>
        /// <returns></returns>
        public static string Aggregate(this List<IResponse> responses)
        {
            var failure = new StringBuilder();

            for (var index = 0; index < responses.Count; index++)
            {
                var response = responses[index];

                if (response.ResponseState == ResponseState.Success) continue;

                failure.Append(response.Aggregate());
                if (index >= (responses.Count - 1)) continue;
                failure.AppendLine();
            }

            return failure.ToString();
        }

        /// <summary>
        /// Aggregate response failures
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string Aggregate(this IResponse response)
        {
            var failure = new StringBuilder();

            for (var index = 0; index < response.Failures.Count; index++)
            {
                if (index < (response.Failures.Count - 1))
                {
                    failure.AppendLine(response.Failures[index]);
                    continue;
                }

                failure.Append(response.Failures[index]);
            }

            return failure.ToString();
        }

        public static bool Contains(this string source, string toCheck, StringComparison stringComparison)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(toCheck)) return false;

            return source.IndexOf(toCheck, stringComparison) >= 0;
        }
    }
}