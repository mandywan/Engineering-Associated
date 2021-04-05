INSERT INTO Company (CompanyCode, Label, ManagerEmployeeNumber) VALUES ('01', 'Acme Seeds Inc.', 1);
INSERT INTO Company (CompanyCode, Label, ManagerEmployeeNumber) VALUES ('02', 'Acme Planting Ltd.', 6);
INSERT INTO Company (CompanyCode, Label, ManagerEmployeeNumber) VALUES ('03', 'Acme Harvesting Ltd.', 11);

INSERT INTO Office (CompanyCode, OfficeCode, Label, ManagerEmployeeNumber) VALUES ('01', '01', 'Corporate', 1);
INSERT INTO Office (CompanyCode, OfficeCode, Label, ManagerEmployeeNumber) VALUES ('02', '02', 'Vancouver', 8);
INSERT INTO Office (CompanyCode, OfficeCode, Label, ManagerEmployeeNumber) VALUES ('03', '03', 'Kelowna', 12);
INSERT INTO Office (CompanyCode, OfficeCode, Label, ManagerEmployeeNumber) VALUES ('03', '04', 'Prince George', 13);
INSERT INTO Office (CompanyCode, OfficeCode, Label, ManagerEmployeeNumber) VALUES ('03', '05', 'Victoria', 13);

INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('01', '01', '01', 'Administration', 2);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('01', '01', '02', 'Marketing', 3);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('01', '01', '03', 'Sales', 4);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('01', '01', '04', 'Accounting', 5);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('01', '01', '05', 'Human Resources', 5);

INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('02', '02', '01', 'Administration', 6);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('02', '02', '02', 'Marketing', 7);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('02', '02', '03', 'Database', 10);

INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('03', '04', '04', 'Sales', 8);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('03', '04', '05', 'IT', 16);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('03', '04', '06', 'Service', 9);

INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('03', '03', '01', 'Administration', 11);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('03', '03', '03', 'Marketing & Sales', 12);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('03', '03', '04', 'Distribution', 13);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('03', '03', '05', 'Sales', 14);

INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('03', '05', '09', 'Operations', 15);

INSERT INTO Location (LocationId, Label, SortValue) VALUES ('01', 'Burnaby', 'A');
INSERT INTO Location (LocationId, Label, SortValue) VALUES ('02', 'Vancouver', 'B');
INSERT INTO Location (LocationId, Label, SortValue) VALUES ('03', 'Kelowna', 'C');
INSERT INTO Location (LocationId, Label, SortValue) VALUES ('04', 'Prince George', 'D');
INSERT INTO Location (LocationId, Label, SortValue) VALUES ('05', 'Victoria', 'D');
INSERT INTO Location (LocationId, Label, SortValue) VALUES ('06', 'Surrey', 'D');

INSERT INTO Category (SkillCategoryId, Label, SortValue) VALUES ('1', 'Agriculture', 'A');
INSERT INTO Category (SkillCategoryId, Label, SortValue) VALUES ('2', 'Accounting', 'A');
INSERT INTO Category (SkillCategoryId, Label, SortValue) VALUES ('3', 'Management', 'A');
INSERT INTO Category (SkillCategoryId, Label, SortValue) VALUES ('4', 'Marketing & Sales', 'A');

INSERT INTO Skill (SkillCategoryId, SkillId, Label, SortValue) VALUES ('1', '1', 'Planting', 'B');
INSERT INTO Skill (SkillCategoryId, SkillId, Label, SortValue) VALUES ('1', '2', 'Harvesting', 'E');
INSERT INTO Skill (SkillCategoryId, SkillId, Label, SortValue) VALUES ('1', '3', 'Fertilizing', 'C');
INSERT INTO Skill (SkillCategoryId, SkillId, Label, SortValue) VALUES ('1', '4', 'Irrigating', 'D');
INSERT INTO Skill (SkillCategoryId, SkillId, Label, SortValue) VALUES ('2', '6', 'Maths', 'A');
INSERT INTO Skill (SkillCategoryId, SkillId, Label, SortValue) VALUES ('2', '7', 'Financial Statements', 'A');
INSERT INTO Skill (SkillCategoryId, SkillId, Label, SortValue) VALUES ('2', '8', 'Statement Analysis', 'A');
INSERT INTO Skill (SkillCategoryId, SkillId, Label, SortValue) VALUES ('2', '9', 'Projection', 'A');
INSERT INTO Skill (SkillCategoryId, SkillId, Label, SortValue) VALUES ('3', '10', 'People Skills', 'A');
INSERT INTO Skill (SkillCategoryId, SkillId, Label, SortValue) VALUES ('3', '11', 'Conflict Resolution', 'A');
INSERT INTO Skill (SkillCategoryId, SkillId, Label, SortValue) VALUES ('3', '12', 'Work Safe', 'A');
INSERT INTO Skill (SkillCategoryId, SkillId, Label, SortValue) VALUES ('3', '13', 'Wage Negotiation', 'A');
INSERT INTO Skill (SkillCategoryId, SkillId, Label, SortValue) VALUES ('4', '14', 'Marketing', 'A');
INSERT INTO Skill (SkillCategoryId, SkillId, Label, SortValue) VALUES ('4', '15', 'Sales', 'A');
INSERT INTO Skill (SkillCategoryId, SkillId, Label, SortValue) VALUES ('4', '16', 'Customer Service', 'A');

/* insert admin */
INSERT INTO Admin (Username, Password) VALUES ('admin', '123456');

