// modified from https://github.com/mtz8302/AGO_SectionControl_SW_arduino_nano

void ReadRateSwitch(byte swMin,byte swMax)
{
	RateUpPressed = !digitalRead(RateUpPin);
	RateDownPressed = !digitalRead(RateDownPin);

	if (RateUpPressed || RateDownPressed)
	{
		// rate switch pressed
		SWreadTime = millis();

		if (SWreadTime - RateLastTime > RateDelayTime)
		{
			RateLastTime = SWreadTime;
			if (AutoOn)
			{
				if (RateUpPressed)
				{
					if (bitRead(OutCommand, 2))
					{
						if (!bitRead(OutCommand, 3))
						{
							bitSet(OutCommand, 3);
							bitClear(OutCommand, 2);
						}
					}
					else
					{
						bitSet(OutCommand, 2);
					}
					bitClear(OutCommand, 4); // left
					bitSet(OutCommand, 5); // rate up
				}

				if (RateDownPressed)
				{
					if (bitRead(OutCommand, 2))
					{
						if (!bitRead(OutCommand, 3))
						{
							bitSet(OutCommand, 3);
							bitClear(OutCommand, 2);
						}
					}
					else
					{
						bitSet(OutCommand, 2);
					}
					bitClear(OutCommand, 4); // left
					bitClear(OutCommand, 5); // rate down
				}
			}
			else
			{
				// manual rate adjustment
				int HalfWay = swMin + (swMax - swMin) / 2;

				if (pwmSettingManual < swMin)
				{
					pwmSettingManual = swMin;
				}
				else if ((pwmSettingManual >= swMax	) && (pwmSettingManual < HalfWay))
				{
					pwmSettingManual = HalfWay;
				}
				else if (pwmSettingManual >= HalfWay)
				{
					pwmSettingManual = swMax;
				}

				if (pwmSettingManual < swMin) pwmSettingManual = swMin;
				if (pwmSettingManual > swMax) pwmSettingManual = swMax;

				if (RateUpPressed)
				{
					pwmSetting = pwmSettingManual;
				}
				else
				{
					pwmSetting = pwmSettingManual * -1;
				}
			}
		}
	}
	else
	{
		// rate switch not pressed

		if (!AutoOn)
		{
			// reset manual values
			pwmSettingManual = 0;
			pwmSetting = 0;
		}

		RateLastTime = millis() - RateDelayTime;

		if (SWreadTime - RateLastTime > SWdelay)
		{
			// clear rate values after delay
			bitClear(OutCommand, 2);
			bitClear(OutCommand, 3);
			bitClear(OutCommand, 4);
			bitClear(OutCommand, 5);
		}
	}
}




