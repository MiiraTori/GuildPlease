using System;

[System.Serializable]
public class GameTime
{
    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;

    public GameTime(int year = 1, int month = 1, int day = 1, int hour = 0, int minute = 0)
    {
        this.year = year;
        this.month = month;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }

    public void AddMinutes(int minutesToAdd)
    {
        minute += minutesToAdd;

        while (minute >= 60)
        {
            minute -= 60;
            hour++;
        }

        while (hour >= 24)
        {
            hour -= 24;
            day++;
        }

        // 簡略化：30日で1ヶ月、12ヶ月で1年
        while (day > 30)
        {
            day -= 30;
            month++;
        }

        while (month > 12)
        {
            month -= 12;
            year++;
        }
    }

    public override string ToString()
    {
        return $"Year {year}, {month:D2}/{day:D2} {hour:D2}:{minute:D2}";
    }
}