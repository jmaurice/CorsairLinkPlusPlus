﻿using CorsairLinkPlusPlus.Driver.Sensor;
using CorsairLinkPlusPlus.Driver.USB;
using System;
using System.Collections.Generic;

namespace CorsairLinkPlusPlus.Driver.Node
{
    public abstract class BaseLinkDevice : BaseDevice
    {
        internal static BaseLinkDevice CreateNew(USB.BaseUSBDevice usbDevice, byte channel, byte deviceType)
        {
            switch (deviceType)
            {
                case 0x5:
                    return new LinkDeviceModern(usbDevice, channel);
                case 0x3:
                    return new LinkDeviceAFP(usbDevice, channel);
                case 0x1:
                    return LinkDevicePSU.CreateNew(usbDevice, channel);
                default:
                    return null;
            }
        }

        public virtual string GetUDID()
        {
            return usbDevice.GetUDID() + "/Channel" + channel;
        }

        internal readonly USB.BaseUSBDevice usbDevice;
        internal readonly byte channel;

        internal BaseLinkDevice(USB.BaseUSBDevice usbDevice, byte channel)
        {
            this.usbDevice = usbDevice;
            this.channel = channel;
        }

        public virtual BaseDevice GetParent()
        {
            return usbDevice;
        }

        public virtual void Refresh(bool volatileOnly)
        {

        }

        public bool IsPresent()
        {
            return true;
        }

        internal byte ReadSingleByteRegister(byte register)
        {
            return usbDevice.ReadSingleByteRegister(register, channel);
        }

        internal byte[] ReadRegister(byte register, byte bytes)
        {
            return usbDevice.ReadRegister(register, channel, bytes);
        }

        internal void WriteSingleByteRegister(byte register, byte value)
        {
            usbDevice.WriteSingleByteRegister(register, channel, value);
        }

        internal void WriteRegister(byte register, byte[] bytes)
        {
            usbDevice.WriteRegister(register, channel, bytes);
        }

        public abstract string GetName();

        public virtual List<BaseDevice> GetSubDevices()
        {
            return new List<BaseDevice>(GetSensors());
        }

        public virtual List<Sensor.BaseSensorDevice> GetSensors()
        {
            return new List<Sensor.BaseSensorDevice>();
        }
    }
}