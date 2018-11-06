using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using CypressSemiconductor.ChinaManufacturingTest;

namespace CyBLE_MTK_Application
{
    class MTKTestDUTCurrentMeasure : MTKTest
    {




        /// <summary>
        /// Test Parameters
        /// 
        /// </summary>
        /// 

        public static double DUTCurrentUpperLimitMilliAmp = CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentUpperLimitMilliAmp;
        public static double DUTCurrentLowerLimitMilliAmp = CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentLowerLimitMilliAmp;
        public static double DUTCurrentDeepSleepLimitMilliAmp = CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentDeepSleepLimitMilliAmp;

        public static string Dmm_Alias = CyBLE_MTK_Application.Properties.Settings.Default.DMM_Alias;
        public static string Switch_Alias = CyBLE_MTK_Application.Properties.Settings.Default.Switch_Alias;
        public static int DelayBeforeTest = CyBLE_MTK_Application.Properties.Settings.Default.DelayInMSBeforeDUTCurrentMeasure;
        public static int DelayAfterTest = CyBLE_MTK_Application.Properties.Settings.Default.DelayInMSBeforeDUTCurrentMeasure;
        public static int SamplesCount = CyBLE_MTK_Application.Properties.Settings.Default.SamplesCountForDUTCurrentMeasure;
        public static string overallpass_condition = CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentMeasureOverallPassCondition;
        public static string criterion_per_sample = CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentMeasureCriteriaPerSample;

        public static int IntervalInMS = CyBLE_MTK_Application.Properties.Settings.Default.IntervalBetweenDUTCurrentMeasure;
        public static string curr_unit = CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentMeasureUnit;



        //private const UInt16 ERRORCODE_DUTCurrentUpperLimitMilliAmp_Failure = 0xA5FF;
        //private const UInt16 ERRORCODE_DUTCurrentLowerLimitMilliAmp_Failure = 0xA500;
        //private const UInt16 ERRORCODE_DUTCurrentDeepSleepLimitMilliAmp_Failure = 0xA511;

        public static UInt16 ERRORCODE_DUTCurrentMeasureFailure;

        private string sample_failure_result_message;

        public MTKTestDUTCurrentMeasure() : base()
        {
            Init();
        }

        public MTKTestDUTCurrentMeasure(LogManager Logger)
            : base(Logger)
        {
            Init();
        }

      public MTKTestDUTCurrentMeasure(LogManager Logger, SerialPort MTKPort, SerialPort DUTPort)
        : base(Logger, MTKPort, DUTPort)
        {
            
            Init();
        }

        



        private void Init()
        {
            NumberOfDUTs = 8;
            CurrentDUT = 0;
            TestParameterCount = 3;

            if (CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentDeepSleepLimitMilliAmp <= 0)
            {
                DUTCurrentDeepSleepLimitMilliAmp = 0.1;
            }
            else
            {
                DUTCurrentDeepSleepLimitMilliAmp = CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentDeepSleepLimitMilliAmp;
            }
            if (CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentLowerLimitMilliAmp <= 0)
            {
                DUTCurrentLowerLimitMilliAmp = 7.0;
            }
            else
            {
                DUTCurrentLowerLimitMilliAmp = CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentLowerLimitMilliAmp;
            }
            if (CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentUpperLimitMilliAmp <= 0)
            {
                DUTCurrentUpperLimitMilliAmp = 20.0;
            }
            else
            {
                DUTCurrentUpperLimitMilliAmp = CyBLE_MTK_Application.Properties.Settings.Default.DUTCurrentUpperLimitMilliAmp;
            }

            InitializeTestResult();

        }

        public override string GetDisplayText()
        {
            return "MTKTestDUTCurrentMeasure: | Range: " + DUTCurrentLowerLimitMilliAmp + " mA" + "~" + DUTCurrentUpperLimitMilliAmp + " mA | " +
                "Delay: {" + DelayBeforeTest + " ms | " +  DelayAfterTest + " ms} | " +
                "Count of Sampling: " + SamplesCount + " | Sample Interval: " + IntervalInMS + " ms | " + 
                "Pass Conditons: {" + criterion_per_sample +  " | " + overallpass_condition +"}";


        }

        public override string GetTestParameter(int TestParameterIndex)
        {
            switch (TestParameterIndex)
            {
                case 0:
                    return DUTCurrentUpperLimitMilliAmp.ToString();
                case 1:
                    return DUTCurrentLowerLimitMilliAmp.ToString();
                case 2:
                    return DUTCurrentDeepSleepLimitMilliAmp.ToString();
            }
            return base.GetTestParameter(TestParameterIndex);
        }

        public override string GetTestParameterName(int TestParameterIndex)
        {
            switch (TestParameterIndex)
            {
                case 0:
                    return "DUTCurrentUpperLimitMilliAmp";
                case 1:
                    return "DUTCurrentLowerLimitMilliAmp";
                case 2:
                    return "DUTCurrentDeepSleepLimitMilliAmp";
            }
            return base.GetTestParameterName(TestParameterIndex);
        }

        public override bool SetTestParameter(int TestParameterIndex, string ParameterValue)
        {
            if (ParameterValue == "")
            {
                return false;
            }
            switch (TestParameterIndex)
            {
                case 0:
                    DUTCurrentUpperLimitMilliAmp = double.Parse(ParameterValue);
                    return true;
                case 1:
                    DUTCurrentLowerLimitMilliAmp = double.Parse(ParameterValue);
                    return true;
                case 2:
                    DUTCurrentDeepSleepLimitMilliAmp = double.Parse(ParameterValue);
                    return true;
            }
            return false;
        }





        private MTKTestError RunTestBLE()
        {
            //TO DO something...

            
            return MTKTestError.NoError;
        }




        private MTKTestError RunTestUART()
        {
            //TO DO something...
            MTKTestError RetVal = MTKTestError.TestFailed;

            try
            {

                MTKInstruments.DUTCurrent = MTKInstruments.MeasureChannelCurrent(CurrentDUT);


                RetVal = MTKTestError.Pending;          //Pending for Pass or Fail but confirm there is no problem on MTKInstruments.MeasureChannelCurrent(CurrentDUT)

                return RetVal;

            }
            catch 
            {
                RetVal = MTKTestError.TestFailed;
                TestResult.Result = "FAIL";
                TestResultUpdate(TestResult);
                return RetVal;
            }
            finally
            {
                //Recover all duts' power on after testing.
                if (!MTKInstruments.SwitchAllDutOn())
                {
                    this.Log.PrintLog(this, "Fail to recover power after current test.", LogDetailLevel.LogRelevant);
                }

            }




        }

        private bool DoesSamplePass(Current dUTCurrent)
        {
            sample_failure_result_message = "Fail";


            if (criterion_per_sample.ToUpper() == EnumPassConPerSample.AVERAGE.ToString())
            {
                if (dUTCurrent.average > DUTCurrentLowerLimitMilliAmp)
                {
                    if (dUTCurrent.average < DUTCurrentUpperLimitMilliAmp)
                    {
                        //Pass
                        ERRORCODE_DUTCurrentMeasureFailure = ECCS.ERRORCODE_ALL_PASS;
                        return true;
                    }
                    else
                    {
                        ERRORCODE_DUTCurrentMeasureFailure = ECCS.ERROR_CODE_DMM_HIGH;
                        sample_failure_result_message += "#" + dUTCurrent.average.ToString("D4");
                        TestStatusUpdate(MTKTestMessageType.Failure, sample_failure_result_message);
                    }
                    
                }
                else
                {
                    ERRORCODE_DUTCurrentMeasureFailure = ECCS.ERROR_CODE_DMM_LOW;
                    sample_failure_result_message += "#" + dUTCurrent.average.ToString("D4");
                    TestStatusUpdate(MTKTestMessageType.Failure, sample_failure_result_message);
                }
            }
            else
            {
                if (dUTCurrent.max > DUTCurrentLowerLimitMilliAmp)
                {
                    if (dUTCurrent.min < DUTCurrentUpperLimitMilliAmp)
                    {
                        //Pass
                        ERRORCODE_DUTCurrentMeasureFailure = ECCS.ERRORCODE_ALL_PASS;
                        return true;
                    }
                    else
                    {
                        ERRORCODE_DUTCurrentMeasureFailure = ECCS.ERROR_CODE_DMM_HIGH;
                        sample_failure_result_message += "#" + dUTCurrent.average.ToString("D4");
                        TestStatusUpdate(MTKTestMessageType.Failure, sample_failure_result_message);
                    }
                    
                }
                else
                {
                    ERRORCODE_DUTCurrentMeasureFailure = ECCS.ERROR_CODE_DMM_LOW;
                    sample_failure_result_message += "#" + dUTCurrent.average.ToString("D4");
                    TestStatusUpdate(MTKTestMessageType.Failure, sample_failure_result_message);
                }

            }



            return false;
        }

        public override MTKTestError RunTest()
        {
            MTKTestError RetVal = MTKTestError.TestFailed;

            this.InitializeTestResult();

            if (this.DUTConnectionMode == DUTConnMode.BLE)
            {
                RetVal = RunTestBLE();
            }
            else if (this.DUTConnectionMode == DUTConnMode.UART)
            {
                int loop = SamplesCount;

                if (overallpass_condition.ToUpper() == EnumPassConOverall.SINGLE_PASS.ToString())
                {
                    while (loop >= 0)
                    {
                        RetVal = RunTestUART();
                        loop--;
                        if (DoesSamplePass(MTKInstruments.DUTCurrent)&& RetVal == MTKTestError.Pending)
                        {
                            RetVal = MTKTestError.NoError;
                            break;
                        }
                        
                    }
                }
                else
                {
                    
                    while (loop > 0)
                    {
                        RetVal = RunTestUART();
                        loop--;
                        if (!DoesSamplePass(MTKInstruments.DUTCurrent) && RetVal == MTKTestError.Pending)
                        {
                            RetVal = MTKTestError.TestFailed;
                            break;
                        }
                        else
                        {
                            TestStatusUpdate(MTKTestMessageType.Success, "Pass: " + (SamplesCount-loop).ToString() + "/" + SamplesCount.ToString());
                            RetVal = MTKTestError.NoError;
                            continue;
                        }

                    }
                }
                

                //Overall TestResult
                if (RetVal == MTKTestError.NoError)
                {
                    TestStatusUpdate(MTKTestMessageType.Success, "Pass");
                    TestResult.Result = "Pass";
                }
                else
                {
                    TestStatusUpdate(MTKTestMessageType.Failure, "Fail");
                    TestResult.Result = "FAIL";
                    
                }

            }            
            else
            {
                TestStatusUpdate(MTKTestMessageType.Failure, "NoConnectionModeSet");
                return MTKTestError.NoConnectionModeSet;
            }

            TestResultUpdate(TestResult);

            return RetVal;
        }

        protected override void InitializeTestResult()
        {
            base.InitializeTestResult();
            TestResult.PassCriterion = "Limit: " +  + Math.Round(DUTCurrentLowerLimitMilliAmp,3) + "~" + Math.Round(DUTCurrentUpperLimitMilliAmp, 3) + " mA" +"||" + criterion_per_sample +"||"+ overallpass_condition;
            TestResult.Measured = "N/A";
            ERRORCODE_DUTCurrentMeasureFailure = ECCS.ERRORCODE_ALL_PASS;
            sample_failure_result_message = "";

            CurrentMTKTestType = MTKTestType.MTKTestDUTCurrentMeasure;
        }


    }

    public enum EnumPassConPerSample
    {
        AVERAGE = 0,    //Check average value
        MAX_AND_MIN = 1 //Check Max and Min average
    };

    public struct Current
    {
        public double average;
        public double max;
        public double min;

        public CurrentUnit unit;


    }
}
