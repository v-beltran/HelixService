<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:dt="urn:dt">
  <xsl:output method="html" omit-xml-declaration="yes"/>
  <xsl:template match="/">
    <xsl:variable name="currentMonth" select="dt:GetDateTime(year/@value, year/month/@value, 1)" />
    <xsl:variable name="prevMonth">
      <xsl:choose>
        <xsl:when test="year/month/@value - 1 = '0'">
          <xsl:value-of select="dt:GetDateTime(year/@value - 1, 12, 1)" />
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="dt:GetDateTime(year/@value, year/month/@value - 1, 1)" />
        </xsl:otherwise>
      </xsl:choose>
    </xsl:variable>
    <xsl:variable name="nextMonth">
      <xsl:choose>
        <xsl:when test="year/month/@value + 1 = '13'">
          <xsl:value-of select="dt:GetDateTime(year/@value + 1, 1, 1)" />
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="dt:GetDateTime(year/@value, year/month/@value + 1, 1)" />
        </xsl:otherwise>
      </xsl:choose>
    </xsl:variable>
    <table class="calendar">
      <thead>
        <tr class="calendar-header">
          <th colspan="1">
            <a id="prev" data-month="{$prevMonth}">←</a>
          </th>
          <th colspan="5" class="month">
            <xsl:value-of select="dt:GetMonthName(year/month/@value)"/>
          </th>
          <th colspan="1">
            <a id="next" data-month="{$nextMonth}">→</a>
          </th>
        </tr>
        <tr class="week">
          <th>
            Sunday
          </th>
          <th>
            Monday
          </th>
          <th>
            Tuesday
          </th>
          <th>
            Wednesday
          </th>
          <th>
            Thursday
          </th>
          <th>
            Friday
          </th>
          <th>
            Saturday
          </th>
        </tr>
      </thead>
      <tfoot></tfoot>
      <tbody>
        <xsl:for-each select="year/month/week">
          <tr class="days">
            <td>
              <xsl:if test="sunday">
                <span class="day">
                  <xsl:value-of select="sunday/@value"/>
                </span>
              </xsl:if>
              <xsl:if test="event">
                <div class="event">
                  <xsl:value-of select="event"/>
                </div>
              </xsl:if>
            </td>
            <td>
              <xsl:if test="monday">
                <span class="day">
                  <xsl:value-of select="monday/@value"/>
                </span>
              </xsl:if>
              <xsl:if test="event">
                <div class="event">
                  <xsl:value-of select="event"/>
                </div>
              </xsl:if>
            </td>
            <td>
              <xsl:if test="tuesday">
                <span class="day">
                  <xsl:value-of select="tuesday/@value"/>
                </span>
              </xsl:if>
              <xsl:if test="event">
                <div class="event">
                  <xsl:value-of select="event"/>
                </div>
              </xsl:if>
            </td>
            <td>
              <xsl:if test="wednesday">
                <span class="day">
                  <xsl:value-of select="wednesday/@value"/>
                </span>
              </xsl:if>
              <xsl:if test="event">
                <div class="event">
                  <xsl:value-of select="event"/>
                </div>
              </xsl:if>
            </td>
            <td>
              <xsl:if test="thursday">
                <span class="day">
                  <xsl:value-of select="thursday/@value"/>
                </span>
              </xsl:if>
              <xsl:if test="event">
                <div class="event">
                  <xsl:value-of select="event"/>
                </div>
              </xsl:if>
            </td>
            <td>
              <xsl:if test="friday">
                <span class="day">
                  <xsl:value-of select="friday/@value"/>
                </span>
              </xsl:if>
              <xsl:if test="event">
                <div class="event">
                  <xsl:value-of select="event"/>
                </div>
              </xsl:if>
            </td>
            <td>
              <xsl:if test="saturday">
                <span class="day">
                  <xsl:value-of select="saturday/@value"/>
                </span>
              </xsl:if>
              <xsl:if test="event">
                <div class="event">
                  <xsl:value-of select="event"/>
                </div>
              </xsl:if>
            </td>
          </tr>
        </xsl:for-each>
      </tbody>
    </table>
  </xsl:template>
</xsl:stylesheet>
