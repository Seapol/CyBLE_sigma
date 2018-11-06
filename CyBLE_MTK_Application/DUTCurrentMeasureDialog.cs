using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CyBLE_MTK_Application
{
    public partial class DUTCurrentMeasureDialog : Form
    {

        //private MTKTestDUTCurrentMeasure _DutCurrMeas;

        public DUTCurrentMeasureDialog()
        {
            InitializeComponent();

            numericUpDownSampleInterval.Value = CyBLE_MTK_Application.Properties.Settings.Default.IntervalBetweenDUTCurrentMeasure;
            comboBox_overall_condition.Text = CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentMeasureOverallPassCondition;
            numericUpDownDelayBeforeTest.Value = CyBLE_MTK_Application.Properties.Settings.Default.DelayInMSBeforeDUTCurrentMeasure;
            numericUpDownUpperlimit.Value = int.Parse(CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentUpperLimitMilliAmp.ToString());
            numericUpDownLowerlimit.Value = int.Parse(CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentLowerLimitMilliAmp.ToString());
            numericUpDownSampleCount.Value = int.Parse(CyBLE_MTK_Application.Properties.Settings.Default.SamplesCountForDUTCurrentMeasure.ToString());

        }

        private void OKbtn_DUTCurrentMeasureDialog_Click(object sender, EventArgs e)
        {
            MTKTestDUTCurrentMeasure.DUTCurrentUpperLimitMilliAmp = double.Parse(numericUpDownUpperlimit.Text);
            MTKTestDUTCurrentMeasure.DUTCurrentLowerLimitMilliAmp = double.Parse(numericUpDownLowerlimit.Text);
            MTKTestDUTCurrentMeasure.DelayBeforeTest = int.Parse(numericUpDownDelayBeforeTest.Text);
            MTKTestDUTCurrentMeasure.DelayAfterTest = int.Parse(numericUpDownDelayAfterTest.Text);
            MTKTestDUTCurrentMeasure.IntervalInMS = int.Parse(numericUpDownSampleInterval.Text);
            MTKTestDUTCurrentMeasure.SamplesCount = int.Parse(numericUpDownSampleCount.Text);
            MTKTestDUTCurrentMeasure.criterion_per_sample = comboBox_criteria_per_sample.Text;
            MTKTestDUTCurrentMeasure.overallpass_condition = comboBox_overall_condition.Text;
            

            //CyBLE_MTK_Application.Properties.Settings.Default.IntervalBetweenDUTCurrentMeasure = int.Parse(numericUpDownInterval.Text);
            //CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentMeasureUnit = comboBox_unit.Text;
            //CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentMeasurePassCondition = comboBox_pass_condition.Text;
            //CyBLE_MTK_Application.Properties.Settings.Default.DelayInMSBeforeDUTCurrentMeasure = int.Parse(numericUpDownDelay.Text);
            //CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentUpperLimitMilliAmp = double.Parse(numericUpDownUpperlimit.Text);
            //CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentLowerLimitMilliAmp = double.Parse(numericUpDownLowerlimit.Text);
            //CyBLE_MTK_Application.Properties.Settings.Default.RepeatForDUTCurrentMeasure = int.Parse(numericUpDownRepeat.Text);

            CyBLE_MTK_Application.Properties.Settings.Default.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDownRepeat_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
