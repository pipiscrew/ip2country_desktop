﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

internal static class DrawingControl
{ //https://stackoverflow.com/a/16625788

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

    private const int WM_SETREDRAW = 11;

    /// <summary>
    /// Some controls, such as the DataGridView, do not allow setting the DoubleBuffered property.
    /// It is set as a protected property. This method is a work-around to allow setting it.
    /// Call this in the constructor just after InitializeComponent().
    /// </summary>
    /// <param name="control">The Control on which to set DoubleBuffered to true.</param>
    public static void SetDoubleBuffered(Control control)
    {
        // if not remote desktop session then enable double-buffering optimization
        if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
        {

            // set instance non-public property with name "DoubleBuffered" to true
            typeof(Control).InvokeMember("DoubleBuffered",
                                         System.Reflection.BindingFlags.SetProperty |
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic,
                                         null,
                                         control,
                                         new object[] { true });
        }
    }

    /// <summary>
    /// Suspend drawing updates for the specified control. After the control has been updated
    /// call DrawingControl.ResumeDrawing(Control control).
    /// </summary>
    /// <param name="control">The control to suspend draw updates on.</param>
    public static void SuspendDrawing(Control control)
    {
        SendMessage(control.Handle, WM_SETREDRAW, false, 0);
    }

    /// <summary>
    /// Resume drawing updates for the specified control.
    /// </summary>
    /// <param name="control">The control to resume draw updates on.</param>
    public static void ResumeDrawing(Control control)
    {
        SendMessage(control.Handle, WM_SETREDRAW, true, 0);
        control.Refresh();
    }
}