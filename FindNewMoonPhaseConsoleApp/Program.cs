using FindNewMoonPhaseConsoleApp;

Console.Write("Looking for new moon phases in the date range...");
Console.WriteLine(Environment.NewLine);

DateTime startDate = new(2000, 1, 6); // first new moon of 2000 (k == 0)
DateTime endDate = new(2050, 12, 14); // last new moon of 2050

foreach (var d in Utils.EachLunarCycle(startDate, endDate))
{
    int k = Utils.ToK(d);                   // (49.2)
    var T = Utils.ToT(k);                   // (49.3)  
    var JDE = Utils.ToJDE(k, T);            // (49.1)
    var E = Utils.ToE(T);                   // (47.6)
    var Sm = Utils.ToM(k, T);               // (49.4)
    var Mm = Utils.ToMPrime(k, T);          // (49.5)
    var F = Utils.ToF(k, T);                // (49.6)
    var O = Utils.ToO(k, T);                // (49.7)
    var A = Utils.ToA(k, T);                // (pp. 351-2)
    var NM = Utils.ToNM(E, Sm, Mm, F, O);   // (p. 351)

    var correctedJDE = JDE + NM + A;
    var newMoonDateTime = Utils.ToDateTimeUTC(correctedJDE);
    var newMoonDateTimeDisplay = Utils.ToDateTimeUTCDisplay(newMoonDateTime);

    Console.WriteLine("{0}", newMoonDateTimeDisplay);
}

Console.WriteLine(Environment.NewLine);
Console.Write("Press ENTER to quit.");
Console.ReadKey();