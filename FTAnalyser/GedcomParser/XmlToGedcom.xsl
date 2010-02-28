<xsl:transform
 xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
 version="1.0"
>

<xsl:output method="saxon:GedcomOutputter" xmlns:saxon="http:/icl.com/saxon"/>

<xsl:template match="*">
    <xsl:copy>
        <xsl:for-each select="@*">
            <xsl:copy/>
        </xsl:for-each>
        <xsl:apply-templates/>
    </xsl:copy> 
</xsl:template>

</xsl:transform>	
