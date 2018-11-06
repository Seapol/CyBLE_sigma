Release notes:


2018-07-23 (v4.0.1)
1. fix the bug about error code for delay is not ALL_PASS but test ignored which casued the final error code is 0001
2. Add RedirectTheFirstFinalRowIndexfromDUTInfoDataGridView() in the CyBLEMTKMainForm whenever click TestRunStopButton before everything test
	a.To get the first and final row form DUTInfoDataGridView
	b.Let ProgramAllatBegin and ProgramAllatEnd work correctly
	c.This added feature allows user to no need to configure actual 1st row and final row in the DUTInfoDataGridView in some scenario
3. Add a reminder Messagebox for missing test althrough Test result pass

2017-11-15 (v1.4.2):
1.Add DUTCurrentMeasureDialog
	a. Able to config settings of DUTCurrentMeasureDialog
2.Add Switch_Config_Dialog in the CyBLEMTKMainForm's menustrip --> Tool --> Devices Setup for DUTCurrentMeasure
	a. Able to config Switch Matrix's alias and try to connect
	b. Able to save the alias
2.Add DMM_Config_Dialog in the CyBLEMTKMainForm's menustrip --> Tool --> Devices Setup for DUTCurrentMeasure
	a. Able to config DMM's alias and try to connect
	b. Able to save the alias
