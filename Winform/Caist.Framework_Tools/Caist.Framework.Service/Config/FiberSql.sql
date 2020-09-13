/*摆哈光纤测温sql*/
select i.id,i.sectionName as AreaName,d.tempmin as MinValue,d.tempmax as MaxValue,d.tempavg as AverageValue,
d.recordtime as CreateTime from TB_DTS_SectionInfo i inner join TB_DTS_SectionTempData d on i.id=d.sectionId
order by i.id