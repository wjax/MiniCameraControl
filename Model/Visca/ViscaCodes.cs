﻿using System;

namespace MiniCameraControl.Model.Visca
{
    public static class ViscaCodes
    {
        public const byte HEADER = 0x80;
        public const byte COMMAND = 0x01;
        public const byte INQUIRY = 0x09;
        public const byte TERMINATOR = 0xFF;
                     
        public const byte CATEGORY_CAMERA1 = 0x04;
        public const byte CATEGORY_PAN_TILTER = 0x06;
        public const byte CATEGORY_CAMERA2 = 0x07;
                     
        public const byte SUCCESS = 0x00;
        public const byte FAILURE = 0xFF;
                     
        // Response tbyte 
        public const byte RESPONSE_CLEAR = 0x40;
        public const byte RESPONSE_ADDRESS = 0x30;
        public const byte RESPONSE_ACK = 0x40;
        public const byte RESPONSE_COMPLETED = 0x50;
        public const byte RESPONSE_ERROR = 0x60;
        public const byte RESPONSE_TIMEOUT = 0x70;  // Not offical, I created this to handle serial port timeouts
                     
        // Commands/ibyteries codes
        public const byte POWER = 0x00;
        public const byte DEVICE_INFO = 0x02;
        public const byte KEYLOCK = 0x17;
        public const byte ID = 0x22;
        public const byte ZOOM = 0x07;
        public const byte ZOOM_STOP = 0x00;
        public const byte ZOOM_TELE = 0x02;
        public const byte ZOOM_WIDE = 0x03;
        public const byte ZOOM_TELE_SPEED = 0x20;
        public const byte ZOOM_WIDE_SPEED = 0x30;
        public const byte ZOOM_VALUE = 0x47;
        public const byte ZOOM_FOCUS_VALUE = 0x47;
        public const byte DZOOM = 0x06;
        public const byte FOCUS = 0x08;
        public const byte FOCUS_STOP = 0x00;
        public const byte FOCUS_FAR = 0x02;
        public const byte FOCUS_NEAR = 0x03;
        public const byte FOCUS_FAR_SPEED = 0x20;
        public const byte FOCUS_NEAR_SPEED = 0x30;
        public const byte FOCUS_VALUE = 0x48;
        public const byte FOCUS_AUTO = 0x38;
        public const byte FOCUS_AUTO_MAN = 0x10;
        public const byte FOCUS_ONE_PUSH = 0x18;
        public const byte FOCUS_ONE_PUSH_TRIG = 0x01;
        public const byte FOCUS_ONE_PUSH_INF = 0x02;
        public const byte FOCUS_AUTO_SENSE = 0x58;
        public const byte FOCUS_AUTO_SENSE_HIGH = 0x02;
        public const byte FOCUS_AUTO_SENSE_LOW = 0x03;
        public const byte FOCUS_NEAR_LIMIT = 0x28;
        public const byte WB = 0x35;
        public const byte WB_AUTO = 0x00;
        public const byte WB_INDOOR = 0x01;
        public const byte WB_OUTDOOR = 0x02;
        public const byte WB_ONE_PUSH = 0x03;
        public const byte WB_ATW = 0x04;
        public const byte WB_MANUAL = 0x05;
        public const byte WB_ONE_PUSH_TRIG = 0x05;
        public const byte RGAIN = 0x03;
        public const byte RGAIN_VALUE = 0x43;
        public const byte BGAIN = 0x04;
        public const byte BGAIN_VALUE = 0x44;
        public const byte AUTO_EXP = 0x39;
        public const byte AUTO_EXP_FULL_AUTO = 0x00;
        public const byte AUTO_EXP_MANUAL = 0x03;
        public const byte AUTO_EXP_SHUTTER_PRIORITY = 0x0A;
        public const byte AUTO_EXP_IRIS_PRIORITY = 0x0B;
        public const byte AUTO_EXP_GAIN_PRIORITY = 0x0C;
        public const byte AUTO_EXP_BRIGHT = 0x0D;
        public const byte AUTO_EXP_SHUTTER_AUTO = 0x1A;
        public const byte AUTO_EXP_IRIS_AUTO = 0x1B;
        public const byte AUTO_EXP_GAIN_AUTO = 0x1C;
        public const byte SLOW_SHUTTER = 0x5A;
        public const byte SLOW_SHUTTER_AUTO = 0x02;
        public const byte SLOW_SHUTTER_MANUAL = 0x03;
        public const byte SHUTTER = 0x0A;
        public const byte SHUTTER_VALUE = 0x4A;
        public const byte IRIS = 0x0B;
        public const byte IRIS_VALUE = 0x4B;
        public const byte GAIN = 0x0C;
        public const byte GAIN_VALUE = 0x4C;
        public const byte BRIGHT = 0x0D;
        public const byte BRIGHT_VALUE = 0x4D;
        public const byte EXP_COMP = 0x0E;
        public const byte EXP_COMP_POWER = 0x3E;
        public const byte EXP_COMP_VALUE = 0x4E;
        public const byte BACKLIGHT_COMP = 0x33;
        public const byte APERTURE = 0x02;
        public const byte APERTURE_VALUE = 0x42;
        public const byte ZERO_LUX = 0x01;
        public const byte IR_LED = 0x31;
        public const byte WIDE_MODE = 0x60;
        public const byte WIDE_MODE_OFF = 0x00;
        public const byte WIDE_MODE_CINEMA = 0x01;
        public const byte WIDE_MODE_16_9 = 0x02;
        public const byte MIRROR = 0x61;
        public const byte FREEZE = 0x62;
        public const byte PICTURE_EFFECT = 0x63;
        public const byte PICTURE_EFFECT_OFF = 0x00;
        public const byte PICTURE_EFFECT_PASTEL = 0x01;
        public const byte PICTURE_EFFECT_NEGATIVE = 0x02;
        public const byte PICTURE_EFFECT_SEPIA = 0x03;
        public const byte PICTURE_EFFECT_BW = 0x04;
        public const byte PICTURE_EFFECT_SOLARIZE = 0x05;
        public const byte PICTURE_EFFECT_MOSAIC = 0x06;
        public const byte PICTURE_EFFECT_SLIM = 0x07;
        public const byte PICTURE_EFFECT_STRETCH = 0x08;
        public const byte DIGITAL_EFFECT = 0x64;
        public const byte DIGITAL_EFFECT_OFF = 0x00;
        public const byte DIGITAL_EFFECT_STILL = 0x01;
        public const byte DIGITAL_EFFECT_FLASH = 0x02;
        public const byte DIGITAL_EFFECT_LUMI = 0x03;
        public const byte DIGITAL_EFFECT_TRAIL = 0x04;
        public const byte DIGITAL_EFFECT_LEVEL = 0x65;
        public const byte MEMORY = 0x3F;
        public const byte MEMORY_RESET = 0x00;
        public const byte MEMORY_SET = 0x01;
        public const byte MEMORY_RECALL = 0x02;
        public const byte DISPLAY = 0x15;
        public const byte DISPLAY_TOGGLE = 0x10;
        public const byte DATE_TIME_SET = 0x70;
        public const byte DATE_DISPLAY = 0x71;
        public const byte TIME_DISPLAY = 0x72;
        public const byte TITLE_DISPLAY = 0x74;
        public const byte TITLE_DISPLAY_CLEAR = 0x00;
        public const byte TITLE_SET = 0x73;
        public const byte TITLE_SET_PARAMS = 0x00;
        public const byte TITLE_SET_PART1 = 0x01;
        public const byte TITLE_SET_PART2 = 0x02;
        public const byte IRRECEIVE = 0x08;
        public const byte IRRECEIVE_ON = 0x02;
        public const byte IRRECEIVE_OFF = 0x03;
        public const byte IRRECEIVE_ONOFF = 0x10;
        public const byte PT_DRIVE = 0x01;
        public const byte PT_DRIVE_HORIZ_LEFT = 0x01;
        public const byte PT_DRIVE_HORIZ_RIGHT = 0x02;
        public const byte PT_DRIVE_HORIZ_STOP = 0x03;
        public const byte PT_DRIVE_VERT_UP = 0x01;
        public const byte PT_DRIVE_VERT_DOWN = 0x02;
        public const byte PT_DRIVE_VERT_STOP = 0x03;
        public const byte PT_ABSOLUTE_POSITION = 0x02;
        public const byte PT_RELATIVE_POSITION = 0x03;
        public const byte PT_HOME = 0x04;
        public const byte PT_RESET = 0x05;
        public const byte PT_LIMITSET = 0x07;
        public const byte PT_LIMITSET_SET = 0x00;
        public const byte PT_LIMITSET_CLEAR = 0x01;
        public const byte PT_LIMITSET_SET_UR = 0x01;
        public const byte PT_LIMITSET_SET_DL = 0x00;
        public const byte PT_DATASCREEN = 0x06;
        public const byte PT_DATASCREEN_ON = 0x02;
        public const byte PT_DATASCREEN_OFF = 0x03;
        public const byte PT_DATASCREEN_ONOFF = 0x10;
                     
        public const byte PT_VIDEOSYSTEM_INQ = 0x23;
        public const byte PT_MODE_INQ = 0x10;
        public const byte PT_MAXSPEED_INQ = 0x11;
        public const byte PT_POSITION_INQ = 0x12;
        public const byte PT_DATASCREEN_INQ = 0x06;
    }
}