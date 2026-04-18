<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes" encoding="UTF-8"/>

  <xsl:key name="employeeKey" match="item" use="concat(@surname, '|', @name)"/>

  <xsl:template match="/Pay">
    <Employees>
      <xsl:apply-templates select="//item[generate-id() = generate-id(key('employeeKey', concat(@surname, '|', @name))[1])]"/>
    </Employees>
  </xsl:template>

  <xsl:template match="item">
    <Employee surname="{@surname}" name="{@name}">
      <xsl:for-each select="key('employeeKey', concat(@surname, '|', @name))">
        <salary mount="{@mount}" amount="{@amount}"/>
      </xsl:for-each>
    </Employee>
  </xsl:template>
</xsl:stylesheet>