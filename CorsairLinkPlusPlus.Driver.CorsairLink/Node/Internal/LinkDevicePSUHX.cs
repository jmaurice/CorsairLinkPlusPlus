﻿
namespace CorsairLinkPlusPlus.Driver.CorsairLink.Node.Internal
{
    public class LinkDevicePSUHX : LinkDevicePSU
    {
        internal LinkDevicePSUHX(USB.BaseUSBDevice usbDevice, byte channel) : base(usbDevice, channel) { }
    }
}