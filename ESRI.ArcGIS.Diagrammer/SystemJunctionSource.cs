/*=============================================================================
 * 
 * Copyright � 2007 ESRI. All rights reserved. 
 * 
 * Use subject to ESRI license agreement.
 * 
 * Unpublished�all rights reserved.
 * Use of this ESRI commercial Software, Data, and Documentation is limited to
 * the ESRI License Agreement. In no event shall the Government acquire greater
 * than Restricted/Limited Rights. At a minimum Government rights to use,
 * duplicate, or disclose is subject to restrictions as set for in FAR 12.211,
 * FAR 12.212, and FAR 52.227-19 (June 1987), FAR 52.227-14 (ALT I, II, and III)
 * (June 1987), DFARS 227.7202, DFARS 252.227-7015 (NOV 1995).
 * Contractor/Manufacturer is ESRI, 380 New York Street, Redlands,
 * CA 92373-8100, USA.
 * 
 * SAMPLE CODE IS PROVIDED "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
 * INCLUDING THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
 * PARTICULAR PURPOSE, ARE DISCLAIMED.  IN NO EVENT SHALL ESRI OR CONTRIBUTORS
 * BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) SUSTAINED BY YOU OR A THIRD PARTY, HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT; STRICT LIABILITY; OR TORT ARISING
 * IN ANY WAY OUT OF THE USE OF THIS SAMPLE CODE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE TO THE FULL EXTENT ALLOWED BY APPLICABLE LAW.
 * 
 * =============================================================================*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.XPath;

namespace ESRI.ArcGIS.Diagrammer {
    /// <summary>
    /// ESRI Junction Feature Source
    /// </summary>
    [Serializable]
    public class SystemJunctionSource : NetworkSource {
        private string _elevationFieldName = string.Empty;
        //
        // CONSTRUCTOR
        //
        public SystemJunctionSource(IXPathNavigable path) : base(path) {
            // Get Navigator
            XPathNavigator navigator = path.CreateNavigator();

            // <ElevationFieldName>
            XPathNavigator navigatorElevationFieldName = navigator.SelectSingleNode("ElevationFieldName");
            if (navigatorElevationFieldName != null) {
                this._elevationFieldName = navigatorElevationFieldName.Value;
            }
        }
        public SystemJunctionSource(SerializationInfo info, StreamingContext context) : base(info, context) {
            this._elevationFieldName = info.GetString("elevationFieldName");
        }
        public SystemJunctionSource(SystemJunctionSource prototype) : base(prototype) {
            this._elevationFieldName = prototype.ElevationFieldName;
        }
        //
        // PROPERTIES
        //
        /// <summary>
        /// The field name to be used as the elevation field when determining connectivity at coincident vertices
        /// </summary>
        [Browsable(true)]
        [Category("Junction Feature Source")]
        [DefaultValue("")]
        [Description("The field name to be used as the elevation field when determining connectivity at coincident vertices")]
        [ParenthesizePropertyName(false)]
        [ReadOnly(false)]
        public string ElevationFieldName {
            get { return this._elevationFieldName; }
            set { this._elevationFieldName = value; }
        }
        //
        // PUBLIC METHODS
        //
        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("elevationFieldName", this._elevationFieldName);

            base.GetObjectData(info, context);
        }
        public override void Errors(List<Error> list, EsriTable table) {
            // TODO Edge Feature Source Errors
        }
        public override object Clone() {
            return new SystemJunctionSource(this);
        }
        public override void WriteXml(XmlWriter writer) {
            // <EdgeFeatureSource>
            writer.WriteStartElement("SystemJunctionSource");
            writer.WriteAttributeString(Xml._XSI, Xml._TYPE, null, "esri:SystemJunctionSource");

            // Writer Inner Xml
            this.WriteInnerXml(writer);

            // </EdgeFeatureSource>
            writer.WriteEndElement();
        }
        protected override void WriteInnerXml(XmlWriter writer) {
            // Write Base Class
            base.WriteInnerXml(writer);

            // <ToElevationFieldName></ToElevationFieldName>
            writer.WriteStartElement("ElevationFieldName");
            writer.WriteValue(this._elevationFieldName);
            writer.WriteEndElement();
        }
    }
}