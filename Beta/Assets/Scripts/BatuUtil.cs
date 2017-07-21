using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

namespace BUtils {

    class LastCallTime {
       public float time = 0;
    }

    public static class Toolbox{

        static readonly Dictionary<string, int> functionDict;
        static readonly LastCallTime lastCallTime;
        static Toolbox() {
            functionDict = new Dictionary<string, int>();
            lastCallTime = new LastCallTime();
        }

        /// <summary>
        /// Returns the first component of type T from the child with tag.
        /// </summary>>
        public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component {
            Transform t = parent.transform;
            foreach (Transform tr in t) {
                if (tr.tag == tag) {
                    return tr.GetComponent<T>();
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the first component of type T from the child with name.
        /// </summary>
        public static T FindComponentInChildWithName<T>(this GameObject parent, string name) where T : Component {
            Transform t = parent.transform;
            foreach (Transform tr in t) {
                if (tr.name == name) {
                    return tr.GetComponent<T>();
                }
            }
            return null;
        }

        /// <summary>
        /// A print function used for debugging. It prints detailed information about the call function.
        /// </summary>
        /// <param name = "param1">Some Parameter.</param>
        /// <returns>No return value.</returns>
        public static void debugPrint(object message = null) {
            StackTrace stackTrace = new StackTrace();
            string callerFunctionName = stackTrace.GetFrame(1).GetMethod().Name;
            string printText = string.Format("Called {0}.", callerFunctionName);
            if (message != null) printText = printText + (" User message: " + message);
            UnityEngine.Debug.Log(printText);
        }


        /// <summary>
        /// Pass in an optional message string to print your message as well.
        /// <para>1 = FUNCTION NAME </para> 
        /// <para>2 = FUNCTION NAME + TIME.</para> 
        /// <para>3 = FUNCTION NAME + COUNT</para> 
        /// <para>4 = FUNCTION NAME + TIME + COUNT.</para> 
        /// </summary>
        /// <returns>No return value.</returns>
        public static void debugPrint(int state, object message = null) {

            string printText = "";
            StackTrace stackTrace = new StackTrace();
            string callerFunctionName = stackTrace.GetFrame(1).GetMethod().Name;
           

            //safety against double counting.
            if (!functionDict.ContainsKey(callerFunctionName)) {
                functionDict.Add(callerFunctionName, 1);
            } else if (lastCallTime.time != Time.time) {
                functionDict[callerFunctionName]++;
                lastCallTime.time = Time.time;
            }

            switch (state) {
                // parent function.
                case 1:
                    printText = string.Format("Called {0}.", callerFunctionName);
                    break;
                // parent function + time
                case 2:
                    printText = string.Format("Called {0}. At time {1}.", callerFunctionName, Time.time);
                    break;
                // parent function + # of times
                case 3:
                    printText = string.Format("Called {0}. {2}th time.", callerFunctionName, Time.time, functionDict[callerFunctionName]);
                    break;
                // parent, time and count.
                case 4:
                    printText = string.Format("Called {0}. At time {1}, {2}th time.", callerFunctionName, Time.time, functionDict[callerFunctionName]);
                    break;
            }

            //Appends the message.
            if (message != null) printText = printText + (" User message: " + message);
            UnityEngine.Debug.Log(printText);
        }

    }
}

