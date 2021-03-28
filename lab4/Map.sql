go
use Test

select top 50 geom, geom.ToString() as WKT, geom.STSrid as SRID from World_Lakes

declare @g geometry = geometry::STGeomFromText('Point(8 -7)', 0);
select @g.STBuffer(5), @g.STBuffer(5).ToString() as WKT from World_Lakes



DECLARE @g1 geometry = 'MULTIPOLYGON EMPTY';  
DECLARE @g2 geometry = 'MULTIPOLYGON(((1 1, 1 -1, -1 -1, -1 1, 1 1)),((1 1, 3 1, 3 3, 1 3, 1 1)))'; 
insert into World_Lakes values(@g2, 'NEW LAKE1')
select * from World_Lakes where name='NEW LAKE1'




declare @g1 geometry; 
select @g1 = geom from World_Hydrography where qgs_fid = 29;
declare @g2 geometry; 
select @g2 = geom from World_Hydrography where qgs_fid = 83;
select @g1.STIntersects(@g2) as [Пересеклось];


go
declare @g3 geometry; 
select @g3 = geom from World_Cities where qgs_fid = 1;
declare @g4 geometry; 
select @g4 = geom from World_Cities where qgs_fid = 2;
Select @g3.STDistance(@g4) as D;



DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 0 1, 1 0, 2 1, 3 0, 4 1)', 0);  
SELECT @g.ToString(), @g.Reduce(.75).ToString();  


