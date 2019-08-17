using System.Collections.Generic;
using Octopus.Core.Data;
using Octopus.Views;
using Rhino;
using Rhino.DocObjects;
using Rhino.FileIO;
using Rhino.PlugIns;
using Rhino.UI;

namespace Octopus
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class OctopusPlugIn : Rhino.PlugIns.PlugIn

    {
        public OctopusPlugIn()
        {
            Instance = this;
        }

        ///<summary>Gets the only instance of the OctopusPlugIn plug-in.</summary>
        public static OctopusPlugIn Instance
        {
            get; private set;
        }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and maintain plug-in wide options in a document.

        protected override void ObjectPropertiesPages(List<ObjectPropertiesPage> pages)
        {
            pages.Add(new SphereObjectPropertiesPage());
            pages.Add(new RectangleObjectPropertiesPage());
            pages.Add(new CurveObjectPropertiesPage());
        }

        protected override LoadReturnCode OnLoad(ref string errorMessage)
        {
            // Add an event handler so we know when documents are opened
            RhinoDoc.EndOpenDocument += RhinoDoc_EndOpenDocument;

            return LoadReturnCode.Success;
        }

        /// <summary>
        /// This gets called when Rhino is finished loading all data from a 3dm file,
        /// so we can go ahead and recreate all custom objects from their geometry and userdata
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RhinoDoc_EndOpenDocument(object sender, DocumentOpenEventArgs e)
        {
            RhinoDoc doc = e.Document;

            foreach (var docObject in doc.Objects)
            {
                // try to find our user data on a given doc object
                var data = docObject.Attributes.UserData.Find(typeof(RectangleData)) as RectangleData;
                if (data is null) continue;

                // Update data to restore auto implemented properties
                data.Update();

                // create a shiny new custom Object from the stored data
                var customObject = data.CreateCustomObject();

                // replace original object in doc
                doc.Objects.Replace(new ObjRef(docObject), customObject);
            }

        }
    }
}