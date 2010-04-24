using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Reflection;
using System.Collections.Generic;

#if SILVERLIGHT
namespace Castle.Silverlight
{

    public static class Extensions
    {
        static object _syncLock = new object();
        static Assembly[] Assemblies;

        /// <summary>
        /// we use the look in the deployment manifest to list and load all the assemblies contained
        /// this should be valid only for the XAP that startup the application, any dynamically
        /// loaded assembly won't be checked.
        /// 
        /// todo: check for a better implementation
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static Assembly[] GetAssemblies(this AppDomain domain)
        {
            if (Assemblies == null)
            {
                lock (_syncLock)
                {
                    if (Assemblies == null)
                    {
                        List<Assembly> assemblies = new List<Assembly>();
                        foreach (System.Windows.AssemblyPart ap in System.Windows.Deployment.Current.Parts)
                        {
                            System.Windows.Resources.StreamResourceInfo sri = System.Windows.Application.GetResourceStream(new Uri(ap.Source, UriKind.Relative));
                            Assembly assembly = new System.Windows.AssemblyPart().Load(sri.Stream);
                            assemblies.Add(assembly);
                        }
                        Assemblies = assemblies.ToArray();
                    }
                }
            }
            return Assemblies;
        }
    }

}
#endif
