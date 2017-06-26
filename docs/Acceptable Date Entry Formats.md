<div class="wikidoc">
<p>The GEDCOM standard has defined the format of dates to allow this information to be passed between Family History Software programs or Analysis Tools like FTAnalyzer.</p>
<p>Even though there is a standard, most FHS allow users to add any date format they want to.&nbsp; This might make sense to the users as they enter the dates but makes it very difficult if you ever need to transfer your tree to another FHS or to use Analysis
 Tools like FTA.</p>
<p>&nbsp;</p>
<p>Here is a list of the date formats from the GEDCOM Specification. All formats in blue are the preferred option and should be used
<strong>before</strong> creating a non standard format.</p>
<p>In the following examples <font color="#0000ff">DATE</font> can be in any of the following formats</p>
<table border="1" cellspacing="5" cellpadding="2" width="501">
<tbody>
<tr>
<td valign="top" width="35"><font color="#0000ff">1885</font></td>
<td valign="top" width="35">&nbsp;</td>
<td valign="top" width="61">&nbsp;</td>
<td valign="top" width="343">Year only</td>
</tr>
<tr>
<td valign="top" width="40"><font color="#0000ff">APR</font></td>
<td valign="top" width="40"><font color="#0000ff">1889</font></td>
<td valign="top" width="63">&nbsp;</td>
<td valign="top" width="334">Month and Year</td>
</tr>
<tr>
<td valign="top" width="44"><font color="#0000ff">22</font></td>
<td valign="top" width="44"><font color="#0000ff">NOV</font></td>
<td valign="top" width="64"><font color="#0000ff">1873</font></td>
<td valign="top" width="327">Day, Month and Year</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<p>To build up a range of dates, the following are used</p>
<table border="1" cellspacing="5" cellpadding="2" width="860">
<tbody>
<tr>
<td valign="top" width="96"><font color="#0000ff">BEF</font></td>
<td valign="top" width="82"><font color="#0000ff">DATE</font></td>
<td valign="top" width="90">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="15">&nbsp;</td>
<td valign="top" width="469">Event happened before the given date</td>
</tr>
<tr>
<td valign="top" width="96"><font color="#0000ff">AFT</font></td>
<td valign="top" width="82"><font color="#0000ff">DATE</font></td>
<td valign="top" width="90">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="15">&nbsp;</td>
<td valign="top" width="469">Event happened after the given date</td>
</tr>
<tr>
<td valign="top" width="96"><font color="#0000ff">BET</font></td>
<td valign="top" width="82"><font color="#0000ff">DATE</font></td>
<td valign="top" width="90"><font color="#0000ff">AND</font></td>
<td valign="top" width="71"><font color="#0000ff">DATE</font></td>
<td valign="top" width="15">&nbsp;</td>
<td valign="top" width="469">Event happened some time between date 1 AND date 2</td>
</tr>
<tr>
<td valign="top" width="96"><font color="#0000ff">FROM</font></td>
<td valign="top" width="82"><font color="#0000ff">DATE</font></td>
<td valign="top" width="90">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="15">&nbsp;</td>
<td valign="top" width="469">Indicates the beginning of a happening or state</td>
</tr>
<tr>
<td valign="top" width="96"><font color="#0000ff">TO</font></td>
<td valign="top" width="82"><font color="#0000ff">DATE</font></td>
<td valign="top" width="90">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="15">&nbsp;</td>
<td valign="top" width="469">Indicates the ending of a happening or state</td>
</tr>
<tr>
<td valign="top" width="96"><font color="#0000ff">FROM</font></td>
<td valign="top" width="82"><font color="#0000ff">DATE</font></td>
<td valign="top" width="90"><font color="#0000ff">TO</font></td>
<td valign="top" width="71"><font color="#0000ff">DATE</font></td>
<td valign="top" width="15">&nbsp;</td>
<td valign="top" width="469">Indicates the beginning and ending of a happening or state</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<p>Because FHS programs don’t enforce users to adhere to the GEDCOM standards, FTAnalyzer can detect and interpret the following date format variations.</p>
<table border="1" cellspacing="5" cellpadding="2" width="911">
<tbody>
<tr>
<td valign="top" width="72">1885</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="72">&nbsp;</td>
<td valign="top" width="69">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Year only</td>
</tr>
<tr>
<td valign="top" width="77">APR</td>
<td valign="top" width="75">1889</td>
<td valign="top" width="77">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Month and Year</td>
</tr>
<tr>
<td valign="top" width="82">22</td>
<td valign="top" width="78">NOV</td>
<td valign="top" width="81">1873</td>
<td valign="top" width="72">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Day, Month and Year</td>
</tr>
<tr>
<td valign="top" width="86">19</td>
<td valign="top" width="80">NOV</td>
<td valign="top" width="84">&nbsp;</td>
<td valign="top" width="72">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Day and Month</td>
</tr>
<tr>
<td valign="top" width="89">BEF</td>
<td valign="top" width="81">DATE</td>
<td valign="top" width="87">&nbsp;</td>
<td valign="top" width="72">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Event happened before the given date</td>
</tr>
<tr>
<td valign="top" width="91">AFT</td>
<td valign="top" width="82">DATE</td>
<td valign="top" width="89">&nbsp;</td>
<td valign="top" width="72">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Event happened after the given date</td>
</tr>
<tr>
<td valign="top" width="93">BET</td>
<td valign="top" width="82">DATE</td>
<td valign="top" width="91">AND</td>
<td valign="top" width="71">DATE</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Event happened some time between date 1 AND date 2</td>
</tr>
<tr>
<td valign="top" width="94">FROM</td>
<td valign="top" width="82">DATE</td>
<td valign="top" width="92">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Indicates the beginning of a happening or state</td>
</tr>
<tr>
<td valign="top" width="95">TO</td>
<td valign="top" width="82">DATE</td>
<td valign="top" width="93">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Indicates the ending of a happening or state</td>
</tr>
<tr>
<td valign="top" width="96">FROM</td>
<td valign="top" width="82">DATE</td>
<td valign="top" width="94">TO</td>
<td valign="top" width="71">DATE</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Indicates the beginning and ending of a happening or state</td>
</tr>
<tr>
<td valign="top" width="96">UNKNOWN</td>
<td valign="top" width="82">&nbsp;</td>
<td valign="top" width="94">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Date has not been specified</td>
</tr>
<tr>
<td valign="top" width="96">1747/48</td>
<td valign="top" width="82">&nbsp;</td>
<td valign="top" width="94">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Double date year, 2 digits</td>
</tr>
<tr>
<td valign="top" width="96">MAR</td>
<td valign="top" width="82">1747/48</td>
<td valign="top" width="94">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Double date year, 2 digits, with Month</td>
</tr>
<tr>
<td valign="top" width="96">11</td>
<td valign="top" width="82">MAR</td>
<td valign="top" width="94">1747/48</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Double date year, 2 digits, with Month and Day</td>
</tr>
<tr>
<td valign="top" width="96">1747/8</td>
<td valign="top" width="82">&nbsp;</td>
<td valign="top" width="94">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Double date year, 1 digit</td>
</tr>
<tr>
<td valign="top" width="96">MAR</td>
<td valign="top" width="82">1747/8</td>
<td valign="top" width="94">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Double date year, 1 digit, with Month</td>
</tr>
<tr>
<td valign="top" width="96">11</td>
<td valign="top" width="82">MAR</td>
<td valign="top" width="94">1747/8</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Double date year, 1 digit, with Month and Day</td>
</tr>
<tr>
<td valign="top" width="96">11</td>
<td valign="top" width="82">MAR</td>
<td valign="top" width="94">1747/1748</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Double date year, 4 digits, with Month and Day</td>
</tr>
<tr>
<td valign="top" width="96">Q3</td>
<td valign="top" width="82">1947</td>
<td valign="top" width="94">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Quarter and Year</td>
</tr>
<tr>
<td valign="top" width="96">1Q</td>
<td valign="top" width="82">1945</td>
<td valign="top" width="94">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Quarter and Year</td>
</tr>
<tr>
<td valign="top" width="96">QTR2</td>
<td valign="top" width="82">1941</td>
<td valign="top" width="94">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Quarter and Year</td>
</tr>
<tr>
<td valign="top" width="96">QTR 4</td>
<td valign="top" width="82">1939</td>
<td valign="top" width="94">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Quarter and Year</td>
</tr>
<tr>
<td valign="top" width="96">SEP</td>
<td valign="top" width="82">QTR</td>
<td valign="top" width="94">1947</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Month, Quarter and Year</td>
</tr>
<tr>
<td valign="top" width="96">JAN</td>
<td valign="top" width="82">FEB</td>
<td valign="top" width="94">MAR</td>
<td valign="top" width="71">1908</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">3 Months and Year</td>
</tr>
<tr>
<td valign="top" width="96">JAN /</td>
<td valign="top" width="82">FEB /</td>
<td valign="top" width="94">MAR /</td>
<td valign="top" width="71">1908</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">3 Months with / and Year</td>
</tr>
<tr>
<td valign="top" width="96">BET</td>
<td valign="top" width="82">JAN-MAR</td>
<td valign="top" width="94">1966</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Quarter Range and Year</td>
</tr>
<tr>
<td valign="top" width="96">BTW</td>
<td valign="top" width="82">1914-1918</td>
<td valign="top" width="94">&nbsp;</td>
<td valign="top" width="71">&nbsp;</td>
<td valign="top" width="19">&nbsp;</td>
<td valign="top" width="571">Year only Range</td>
</tr>
</tbody>
</table>
</div><div class="ClearBoth"></div>
