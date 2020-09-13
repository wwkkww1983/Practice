select r.CurrentStation,
	(select d.StationAddress from Deviceinformation d where r.CurrentStation = d.StationNumber)as StationAddress,
	s.PepoleNumber,s.PepoleName,
	(select t.TypeOfWorkName from TypeOfWork t where t.typeofworkNumber = s.TypeOfWorkNumber) as TypeOfWorkName 
	from RealData r inner join StaffInformation s on r.PepoleNumber = s.PepoleNumber 
	order by StationAddress;