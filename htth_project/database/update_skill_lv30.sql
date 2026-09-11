-- ==========================================================================
-- SQL UPDATE SKILL LEVEL 30 - GAME HTTH
-- Thiet ke lai Data & Hieu ung Skill (L7) khi dat cap do 30 cho 5 he phai
-- ==========================================================================

-- PHUONG AN 1: CHUAN THEO THIET KE SKILL30 (Can bang DPS chuan theo Skill30):

UPDATE `skill` SET `typeEffSkill` = 471, `damage` = 589, `manaLost` = 0, `timeDelay` = 1000, `option` = '[[28,5],[29,170],[30,60]]', `EffSpec` = '[5,120,50]' WHERE `id` = 4065; -- Quả đấm tốc độ (idx: 670, Class 1)
UPDATE `skill` SET `typeEffSkill` = 472, `damage` = 1605, `manaLost` = 9, `timeDelay` = 1100, `option` = '[[10,90],[11,300],[28,5],[29,350],[30,53]]', `EffSpec` = '[5,260,40]' WHERE `id` = 4069; -- Bazooka (idx: 674, Class 1)
UPDATE `skill` SET `typeEffSkill` = 473, `damage` = 1100, `manaLost` = 17, `timeDelay` = 4200, `option` = '[[28,5],[29,120],[30,40]]', `EffSpec` = '[5,120,40]' WHERE `id` = 4073; -- Liên hoàng cú đấm (idx: 678, Class 1)
UPDATE `skill` SET `typeEffSkill` = 481, `damage` = 603, `manaLost` = 0, `timeDelay` = 1000, `option` = '[[11,300],[28,2],[29,120],[30,50]]', `EffSpec` = '[2,120,50]' WHERE `id` = 4077; -- Nhất kiếm (idx: 682, Class 2)
UPDATE `skill` SET `typeEffSkill` = 482, `damage` = 1703, `manaLost` = 12, `timeDelay` = 1100, `option` = '[[13,90],[28,2],[29,300],[30,64]]', `EffSpec` = '[2,300,64]' WHERE `id` = 4081; -- Nhị trảm (idx: 686, Class 2)
UPDATE `skill` SET `typeEffSkill` = 483, `damage` = 1100, `manaLost` = 17, `timeDelay` = 4200, `option` = '[[28,2],[29,120],[30,40]]', `EffSpec` = '[2,120,40]' WHERE `id` = 4085; -- Vòi rồng (idx: 690, Class 2)
UPDATE `skill` SET `typeEffSkill` = 491, `damage` = 623, `manaLost` = 0, `timeDelay` = 1000, `option` = '[[28,4],[29,120],[30,50]]', `EffSpec` = '[4,120,50]' WHERE `id` = 4089; -- Hắc cước (idx: 694, Class 3)
UPDATE `skill` SET `typeEffSkill` = 492, `damage` = 1721, `manaLost` = 9, `timeDelay` = 1100, `option` = '[[10,90],[11,300],[28,4],[29,350],[30,53]]', `EffSpec` = '[4,310,45]' WHERE `id` = 4093; -- Knock out (idx: 698, Class 3)
UPDATE `skill` SET `typeEffSkill` = 493, `damage` = 1200, `manaLost` = 17, `timeDelay` = 4200, `option` = '[[28,3],[29,120],[30,40]]', `EffSpec` = '[3,120,40]' WHERE `id` = 4097; -- Thịt băm (idx: 702, Class 3)
UPDATE `skill` SET `typeEffSkill` = 511, `damage` = 698, `manaLost` = 0, `timeDelay` = 1000, `option` = '[[28,5],[29,300],[30,34]]', `EffSpec` = '[5,300,34]' WHERE `id` = 4101; -- Gậy chong chóng (idx: 706, Class 4)
UPDATE `skill` SET `typeEffSkill` = 512, `damage` = 2012, `manaLost` = 11, `timeDelay` = 1100, `option` = '[[13,90],[28,6],[29,360],[30,100]]', `EffSpec` = '[6,360,100]' WHERE `id` = 4105; -- Bong bóng tích điện (idx: 710, Class 4)
UPDATE `skill` SET `typeEffSkill` = 513, `damage` = 1567, `manaLost` = 18, `timeDelay` = 4200, `option` = '[[28,6],[29,140],[30,60]]', `EffSpec` = '[6,140,60]' WHERE `id` = 4109; -- Bão sấm (idx: 714, Class 4)
UPDATE `skill` SET `typeEffSkill` = 501, `damage` = 612, `manaLost` = 0, `timeDelay` = 1000, `option` = '[[28,4],[29,210],[30,50]]', `EffSpec` = '[4,210,50]' WHERE `id` = 4113; -- Double Shot (idx: 718, Class 5)
UPDATE `skill` SET `typeEffSkill` = 502, `damage` = 2313, `manaLost` = 11, `timeDelay` = 1100, `option` = '[[11,250],[13,90],[28,7],[29,360],[30,80]]', `EffSpec` = '[7,360,80]' WHERE `id` = 4117; -- Quả cầu lửa (idx: 722, Class 5)
UPDATE `skill` SET `typeEffSkill` = 503, `damage` = 1567, `manaLost` = 18, `timeDelay` = 4200, `option` = '[[28,7],[29,130],[30,60]]', `EffSpec` = '[7,130,60]' WHERE `id` = 4121; -- Pháo hoa (idx: 726, Class 5)


-- ==========================================================================
-- PHUONG AN 2: BUFF TOI DA (Giu muc damage cao nhat giua htth_1109 va Skill30):
-- ==========================================================================

-- UPDATE `skill` SET `typeEffSkill` = 471, `damage` = 589, `manaLost` = 0, `timeDelay` = 1000, `option` = '[[28,5],[29,170],[30,60]]', `EffSpec` = '[5,120,50]' WHERE `id` = 4065; -- Quả đấm tốc độ (idx: 670, Class 1)
-- UPDATE `skill` SET `typeEffSkill` = 472, `damage` = 1897, `manaLost` = 9, `timeDelay` = 1100, `option` = '[[10,90],[11,300],[28,5],[29,350],[30,53]]', `EffSpec` = '[5,260,40]' WHERE `id` = 4069; -- Bazooka (idx: 674, Class 1)
-- UPDATE `skill` SET `typeEffSkill` = 473, `damage` = 1231, `manaLost` = 17, `timeDelay` = 4200, `option` = '[[28,5],[29,120],[30,40]]', `EffSpec` = '[5,120,40]' WHERE `id` = 4073; -- Liên hoàng cú đấm (idx: 678, Class 1)
-- UPDATE `skill` SET `typeEffSkill` = 481, `damage` = 603, `manaLost` = 0, `timeDelay` = 1000, `option` = '[[11,300],[28,2],[29,120],[30,50]]', `EffSpec` = '[2,120,50]' WHERE `id` = 4077; -- Nhất kiếm (idx: 682, Class 2)
-- UPDATE `skill` SET `typeEffSkill` = 482, `damage` = 1897, `manaLost` = 12, `timeDelay` = 1100, `option` = '[[13,90],[28,2],[29,300],[30,64]]', `EffSpec` = '[2,300,64]' WHERE `id` = 4081; -- Nhị trảm (idx: 686, Class 2)
-- UPDATE `skill` SET `typeEffSkill` = 483, `damage` = 1222, `manaLost` = 17, `timeDelay` = 4200, `option` = '[[28,2],[29,120],[30,40]]', `EffSpec` = '[2,120,40]' WHERE `id` = 4085; -- Vòi rồng (idx: 690, Class 2)
-- UPDATE `skill` SET `typeEffSkill` = 491, `damage` = 623, `manaLost` = 0, `timeDelay` = 1000, `option` = '[[28,4],[29,120],[30,50]]', `EffSpec` = '[4,120,50]' WHERE `id` = 4089; -- Hắc cước (idx: 694, Class 3)
-- UPDATE `skill` SET `typeEffSkill` = 492, `damage` = 1897, `manaLost` = 9, `timeDelay` = 1100, `option` = '[[10,90],[11,300],[28,4],[29,350],[30,53]]', `EffSpec` = '[4,310,45]' WHERE `id` = 4093; -- Knock out (idx: 698, Class 3)
-- UPDATE `skill` SET `typeEffSkill` = 493, `damage` = 1223, `manaLost` = 17, `timeDelay` = 4200, `option` = '[[28,3],[29,120],[30,40]]', `EffSpec` = '[3,120,40]' WHERE `id` = 4097; -- Thịt băm (idx: 702, Class 3)
-- UPDATE `skill` SET `typeEffSkill` = 511, `damage` = 698, `manaLost` = 0, `timeDelay` = 1000, `option` = '[[28,5],[29,300],[30,34]]', `EffSpec` = '[5,300,34]' WHERE `id` = 4101; -- Gậy chong chóng (idx: 706, Class 4)
-- UPDATE `skill` SET `typeEffSkill` = 512, `damage` = 2012, `manaLost` = 11, `timeDelay` = 1100, `option` = '[[13,90],[28,6],[29,360],[30,100]]', `EffSpec` = '[6,360,100]' WHERE `id` = 4105; -- Bong bóng tích điện (idx: 710, Class 4)
-- UPDATE `skill` SET `typeEffSkill` = 513, `damage` = 1567, `manaLost` = 18, `timeDelay` = 4200, `option` = '[[28,6],[29,140],[30,60]]', `EffSpec` = '[6,140,60]' WHERE `id` = 4109; -- Bão sấm (idx: 714, Class 4)
-- UPDATE `skill` SET `typeEffSkill` = 501, `damage` = 612, `manaLost` = 0, `timeDelay` = 1000, `option` = '[[28,4],[29,210],[30,50]]', `EffSpec` = '[4,210,50]' WHERE `id` = 4113; -- Double Shot (idx: 718, Class 5)
-- UPDATE `skill` SET `typeEffSkill` = 502, `damage` = 2313, `manaLost` = 11, `timeDelay` = 1100, `option` = '[[11,250],[13,90],[28,7],[29,360],[30,80]]', `EffSpec` = '[7,360,80]' WHERE `id` = 4117; -- Quả cầu lửa (idx: 722, Class 5)
-- UPDATE `skill` SET `typeEffSkill` = 503, `damage` = 1567, `manaLost` = 18, `timeDelay` = 4200, `option` = '[[28,7],[29,130],[30,60]]', `EffSpec` = '[7,130,60]' WHERE `id` = 4121; -- Pháo hoa (idx: 726, Class 5)
