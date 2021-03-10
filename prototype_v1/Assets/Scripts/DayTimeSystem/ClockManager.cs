using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum SeasonState { Summer, Fall, Winter, Spring }
enum MeterioLogyState { Sunny, Raining, Storm, Snowing }
enum DayState { Dawn, Morning, Afternoon, Twilight, Night }

public class ClockManager : MonoBehaviour
{
    [Range(360, 1440)] public int dayTime = 1440;

    [Header("Clock Components")]
    public Transform pointer;
    public Image seasonSprite;
    public Image meteriologySprite;
    public Text hoursUI;
    public Text dayUI;
    public Sprite[] ClockStateSprites;
    //StateSprites
    //0 - Summer 
    //1 - Fall 
    //2 - Winter 
    //3 - Spring
    //4 - Sunny
    //5 - Raining
    //6 - Storm
    //7 - Snowing

    private float timePointer, seconds;
    private int minutes, hours, day, year;
    private SeasonState season;
    private MeterioLogyState weather;
    [SerializeField] private DayState moment;

    //methods get-set
    public float TimePointer { get => timePointer; set => timePointer = value; }
    public float Seconds { get => seconds; set => seconds = value; }
    public int Minutes { get => minutes; set => minutes = value; }
    public int Hours { get => hours; set => hours = value; }
    public int Day { get => day; set => day = value; }
    public int Year { get => year; set => year = value; }
    internal SeasonState Season { get => season; set => season = value; }
    internal MeterioLogyState Weather { get => weather; set => weather = value; }
    internal DayState Moment { get => moment; set => moment = value; }

    private void Start()
    {
        RandomizeWeather();
        SetDayTime(23, 40);
        Day = 1;
    }

    private void FixedUpdate()
    {
        UpdateDayTime();
        UpdateDayMoment();
        UpdateWeek();
        UpdateSeason();

        if (Input.GetKeyDown(KeyCode.P)) SetDayTime(5, 40);
    }

    private void UpdatePointerTime()
    {
        pointer.localEulerAngles = Vector3.forward * (180 * timePointer) / dayTime * -1;
    }

    private void UpdateDayTime()
    {
        seconds += Time.deltaTime;

        if (hours >= 0 && hours < 6)
        {
            timePointer = seconds + minutes + (60 * (hours + 18));
        }
        else
        {
            timePointer = seconds + minutes + (60 * (hours - 6));
        }

        if (seconds >= 10)
        {
            minutes += 10;
            seconds = 0;

            if (minutes > 50)
            {
                hours++;
                minutes = 0;

                if (hours > 23)
                {
                    day++;
                    hours = 0;

                    //mudança do tempo temporária
                    RandomizeWeather();

                    if (day > 28 && season == SeasonState.Spring)
                    {
                        year++;
                    }
                }
            }
        }

        UpdatePointerTime();

        string strHour;
        string strMinutes;

        strHour = (int)hours < 10 ? "0" + hours.ToString() : hours.ToString();
        strMinutes = (int)minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();

        hoursUI.text = strHour + ":" + strMinutes;
    }

    private void UpdateDayMoment()
    {
        if (hours >= 0) moment = DayState.Dawn;
        if (hours >= 8) moment = DayState.Morning;
        if (hours >= 12) moment = DayState.Afternoon;
        if (hours >= 16) moment = DayState.Twilight;
        if (hours >= 20) moment = DayState.Night;
    }

    private void UpdateWeek()
    {
        string strDay = day.ToString();
        string strWeekDay = string.Empty;

        if (day % 7 == 1) strWeekDay = ", DOM";
        if (day % 7 == 2) strWeekDay = ", SEG";
        if (day % 7 == 3) strWeekDay = ", TER";
        if (day % 7 == 4) strWeekDay = ", QUA";
        if (day % 7 == 5) strWeekDay = ", QUI";
        if (day % 7 == 6) strWeekDay = ", SEX";
        if (day % 7 == 0) strWeekDay = ", SAB";

        dayUI.text = strDay + strWeekDay;
    }

    private void UpdateSeason()
    {
        if (day > 28)
        {
            if (season == SeasonState.Summer) season = SeasonState.Fall;
            else if (season == SeasonState.Fall) season = SeasonState.Winter;
            else if (season == SeasonState.Winter) season = SeasonState.Spring;
            else if (season == SeasonState.Spring) season = SeasonState.Summer;
        }

        seasonSprite.sprite = ClockStateSprites[(int)season];
    }

    public void RandomizeWeather()
    {
        int lucky = Random.Range(0, 101);

        if (season == SeasonState.Summer)
        {
            //Sunny     60%
            //Raining   25%
            //Storm     15%
            //Snowing   0%

            // Sunny x Others
            if (lucky >= 40) //----------------------- 60% chance
            {
                weather = MeterioLogyState.Sunny;
            }
            else //----------------------------------- 40% chance
            {
                lucky = Random.Range(0, 101);

                //Raining x Storm
                if (lucky >= 35) //------------------- 65% chance
                {
                    weather = MeterioLogyState.Raining;
                }
                else //------------------------------- 35% chance
                {
                    weather = MeterioLogyState.Storm;
                }
            }
        }
        else if (season == SeasonState.Fall)
        {
            //Sunny     50%
            //Raining   45%
            //Storm     5%
            //Snowing   0%

            // Sunny x Others
            if (lucky >= 50) //----------------------- 50% chance
            {
                weather = MeterioLogyState.Sunny;
            }
            else //----------------------------------- 50% chance
            {
                lucky = Random.Range(0, 101);

                //Raining x Storm
                if (lucky >= 10) //------------------- 90% chance
                {
                    weather = MeterioLogyState.Raining;
                }
                else //------------------------------- 10% chance
                {
                    weather = MeterioLogyState.Storm;
                }
            }
        }
        else if (season == SeasonState.Winter)
        {
            //Sunny     50%
            //Raining   0%
            //Storm     5%
            //Snowing   45%

            // Sunny x Others
            if (lucky >= 50) //----------------------- 50% chance
            {
                weather = MeterioLogyState.Sunny;
            }
            else //----------------------------------- 50% chance
            {
                lucky = Random.Range(0, 101);

                //Raining x Storm
                if (lucky >= 10) //------------------- 90% chance
                {
                    weather = MeterioLogyState.Snowing;
                }
                else //------------------------------- 10% chance
                {
                    weather = MeterioLogyState.Storm;
                }
            }
        }
        else if (season == SeasonState.Spring)
        {
            //Sunny     80%
            //Raining   18%
            //Storm     2%
            //Snowing   0%

            // Sunny x Others
            if (lucky >= 20) //----------------------- 80% chance
            {
                weather = MeterioLogyState.Sunny;
            }
            else //----------------------------------- 50% chance
            {
                lucky = Random.Range(0, 101);

                //Raining x Storm
                if (lucky >= 10) //------------------- 90% chance
                {
                    weather = MeterioLogyState.Raining;
                }
                else //------------------------------- 10% chance
                {
                    weather = MeterioLogyState.Storm;
                }
            }
        }

        meteriologySprite.sprite = ClockStateSprites[4 + (int)weather];
    }

    public void SetDayTime(int h, int min)
    {
        seconds = 0;
        timePointer = 0;
        minutes = min;
        hours = h;
    }
}
