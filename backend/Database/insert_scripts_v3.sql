INSERT INTO Company (CompanyCode, Label, ManagerEmployeeNumber) VALUES ('01', 'Acme Seeds Inc.', 1);
INSERT INTO Company (CompanyCode, Label, ManagerEmployeeNumber) VALUES ('02', 'Acme Planting Ltd.', 6);
INSERT INTO Company (CompanyCode, Label, ManagerEmployeeNumber) VALUES ('03', 'Acme Harvesting Ltd.', 11);

INSERT INTO Office (CompanyCode, OfficeCode, Label, ManagerEmployeeNumber) VALUES ('01', '01', 'Corporate', 1);
INSERT INTO Office (CompanyCode, OfficeCode, Label, ManagerEmployeeNumber) VALUES ('02', '01', 'Corporate', 6);
INSERT INTO Office (CompanyCode, OfficeCode, Label, ManagerEmployeeNumber) VALUES ('02', '02', 'Vancouver', 6);
INSERT INTO Office (CompanyCode, OfficeCode, Label, ManagerEmployeeNumber) VALUES ('01', '02', 'Vancouver', 6);
INSERT INTO Office (CompanyCode, OfficeCode, Label, ManagerEmployeeNumber) VALUES ('02', '03', 'Victoria', 7);
INSERT INTO Office (CompanyCode, OfficeCode, Label, ManagerEmployeeNumber) VALUES ('03', '03', 'Victoria', 11);
INSERT INTO Office (CompanyCode, OfficeCode, Label, ManagerEmployeeNumber) VALUES ('03', '04', 'Prince George', 12);

INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('01', '01', '01', 'Administration', 2);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('01', '01', '02', 'Marketing', 3);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('01', '01', '03', 'Sales', 4);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('01', '01', '04', 'Accounting', 5);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('01', '01', '05', 'Human Resources', 5);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('02', '01', '01', 'Administration', 6);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('02', '01', '02', 'Marketing', 7);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('02', '01', '03', 'Marketing', 7);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('02', '02', '04', 'Sales', 8);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('02', '02', '05', 'IT', 8);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('02', '02', '06', 'Service', 9);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('03', '03', '01', 'Administration', 11);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('03', '03', '03', 'Marketing & Sales', 12);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('03', '03', '04', 'Distribution', 13);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('03', '03', '05', 'Sales', 13);
INSERT INTO CompanyOfficeGroup (CompanyCode, OfficeCode, GroupCode, Label, ManagerEmployeeNumber) VALUES ('03', '04', '09', 'Operations', 15);

INSERT INTO Location (LocationId, Label, SortValue) VALUES ('GHHDT1H7', 'Vancouver', 'A');
INSERT INTO Location (LocationId, Label, SortValue) VALUES ('TH8LF9', 'Victoria', 'B');
INSERT INTO Location (LocationId, Label, SortValue) VALUES ('LDFS8F3DDS', 'Kelowna', 'C');
INSERT INTO Location (LocationId, Label, SortValue) VALUES ('JGH7T', 'Prince George', 'D');

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

INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '01', '01', '01', 'Acme', 'Susan', 'Salary', 'President and CEO', '1995-06-01', NULL, 1, 11.2, 'acmes@acme.ca', '604-123-4567', '604-123-7654', 'GHHDT1H7', 'some-url/acmes.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '01', '01', '01', 'Johnson', 'Jill', 'Salary', 'COO', '1997-02-15', NULL, 1, 2.0, 'johnsonj@acme.ca', '604-123-5678', '604-123-8765', 'GHHDT1H7', 'some-url/johnsonj.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '01', '01', '02', 'Sampson', 'Saul', 'Salary', 'Manager-Marketing', '1999-10-01', NULL, 2, 0.0, 'sampsons@acme.ca', '604-123-4567', '604-123-7654', 'GHHDT1H7', 'some-url/sampsons.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '01', '01', '03', 'Da Silva', 'Gregore', 'Salary', 'Manager-Sales', '1998-02-01', NULL, 2, 2.1, 'dasilvag@acme.ca', '604-123-4567', '604-123-7654', 'GHHDT1H7', 'some-url/dasilvag.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '01', '01', '05', 'Conner', 'Connie', 'Salary', 'Manager-HR/Accounting', '1997-05-28', NULL, 2, 3.2, 'connerc@acme.ca', '604-123-4567', '604-123-7654', 'GHHDT1H7', 'some-url/connerc.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '02', '01', '02', 'Broadfoot', 'Brad', 'Salary', 'General Manager', '2001-05-28', NULL, 2, 3.2, 'broadfootb@acme.ca', '604-123-4567', '604-123-7654', 'GHHDT1H7', 'some-url/broadfootb.jpg')
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '02', '01', '02', 'Smithers', 'Sam', 'Salary', 'Operations Manager', '2002-05-28', NULL, 6, 3.2, 'smitherss@acme.ca', '604-123-4567', '604-123-7654', 'TH8LF9', 'some-url/smitherss.jpg');
/* Group Not Inserted*/
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '02', '01', '03', 'Jones', 'Owen', 'Salary', 'Manager-Marketing', '2003-05-28', NULL, 6, 3.2, 'joneso@acme.ca', '604-123-4567', '604-123-7654', 'GHHDT1H7', 'some-url/joneso.jpg')
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '02', '02', '04', 'Westerson', 'Wally', 'Salary', 'Manager-Sales', '2004-05-28', NULL, 6, 3.2, 'westersonw@acme.ca', '604-123-4567', '604-123-7654', 'TH8LF9', 'some-url/westersonw.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '02', '02', '05', 'Masters', 'Mark', 'Salary', 'Manager-Service', '2005-05-28', NULL, 6, 3.2, 'mastersm@acme.ca', '604-123-4567', '604-123-7654', 'TH8LF9', 'some-url/mastersm.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '03', '03', '03', 'Ameras', 'Annie', 'Salary', 'General Manager', '2011-05-28', NULL, 2, 3.2, 'amerasa@acme.ca', '604-123-4567', '604-123-7654', 'LDFS8F3DDS', 'some-url/amerasa.jpg')
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '03', '03', '03', 'Lons', 'Lesley', 'Salary', 'Office Manager - Kelowna', '2012-05-28', NULL, 11, 3.2, 'lonsl@acme.ca', '604-123-4567', '604-123-7654', 'LDFS8F3DDS', 'some-url/lonsl.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '03', '03', '03', 'Pessur', 'Polly', 'Salary', 'Office Manager - Prince George', '2013-05-28', NULL, 11, 3.2, 'pessurp@acme.ca', '250-123-4567', '250-123-7654', 'JGH7T', 'some-url/pessurp.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '03', '03', '04', 'Robson', 'Rocky', 'Salary', 'Manager-Marketing & Sales', '2014-05-28', NULL, 12, 3.2, 'robsonr@acme.ca', '604-123-4567', '604-123-7654', 'LDFS8F3DDS', 'some-url/robsonr.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '03', '03', '05', 'Sanders', 'Sandy', 'Salary', 'Manager-Distribution', '2015-05-28', NULL, 12, 3.2, 'sanderss@acme.ca', '604-123-4567', '604-123-7654', 'LDFS8F3DDS', 'some-url/sanderss.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '03', '04', '09', 'Thompson', 'Tony', 'Salary', 'Manager-Operations', '2016-05-28', NULL, 13, 3.2, 'thompsont@acme.ca', '250-123-4567', '250-123-7654', 'JGH7T', 'some-url/thompsont.jpg');
/* Group Inserted*/
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '01', '01', '01', 'Employee01', 'Name', 'Salary', 'Some title', '2020-01-01', NULL, 1, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'GHHDT1H7', 'some-url/photo_default.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '01', '01', '01', 'Employee02', 'Name', 'Salary', 'Some title', '2020-01-01', NULL, 2, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'GHHDT1H7', 'some-url/photo_default.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '01', '01', '02', 'Employee03', 'Name', 'Salary', 'Some title', '2020-01-01', NULL, 3, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'GHHDT1H7', 'some-url/photo_default.jpg')
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '01', '01', '03', 'Employee04', 'Name', 'Salary', 'Some title', '2020-01-01', NULL, 4, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'GHHDT1H7', 'some-url/photo_default.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '01', '01', '04', 'Employee05', 'Name', 'Salary', 'Some title', '2020-01-01', NULL, 5, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'GHHDT1H7', 'some-url/photo_default.jpg')
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '01', '01', '05', 'Employee06', 'Name', 'Salary', 'Some title', '2020-01-01', NULL, 5, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'GHHDT1H7', 'some-url/photo_default.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '02', '01', '02', 'Employee07', 'Name', 'Salary', 'Some title', '2020-01-01', NULL, 6, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'GHHDT1H7', 'some-url/photo_default.jpg');
/* Group Not Inserted*/
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '02', '02', '04', 'Employee08', 'Name', 'Salary', 'Some title', '2020-01-01', NULL, 7, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'TH8LF9', 'some-url/photo_default.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '02', '01', '03', 'Employee09', 'Name', 'Salary', 'Some title', '2020-01-01', NULL, 8, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'TH8LF9', 'some-url/photo_default.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '02', '02', '04', 'Employee10', 'Name', 'Salary', 'Some title', '2020-01-01', NULL, 9, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'TH8LF9', 'some-url/photo_default.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '02', '02', '05', 'Employee11', 'Name', 'Salary', 'Some title', '2020-01-01', NULL, 9, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'TH8LF9', 'some-url/photo_default.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '03', '03', '03', 'Employee12', 'Name', 'Salary', 'Some title', '2020-01-01', NULL, 11, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'LDFS8F3DDS', 'some-url/photo_default.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '03', '03', '03', 'Employee13', 'Name', 'Salary', 'Some title', '2020-01-01', NULL, 12, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'LDFS8F3DDS', 'some-url/photo_default.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '03', '03', '04', 'Employee14', 'Name', 'Salary', 'Some title', '2020-01-01', NULL, 13, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'LDFS8F3DDS', 'some-url/photo_default.jpg');
/* Contractors */
/* Group Not Inserted*/
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (1, '03', '03', '05', 'Employee15', 'Name', 'Salary', 'Some title', '2020-01-01', NULL, 14, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'LDFS8F3DDS', 'some-url/photo_default.jpg');
/* Group Inserted*/
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (1, '03', '03', '05', 'Employee16', 'Name', 'Salary', 'Some title', '2020-01-01', NULL, 15, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'JGH7T', 'some-url/photo_default.jpg');
/* Terminated employees */
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '01', '01', '01', 'TermEmployee01', 'Name', 'Salary', 'Some title', '2015-01-01', '2018-06-01', 2, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'GHHDT1H7', 'some-url/photo_default.jpg');
/* Group Not Inserted*/
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '02', '02', '04', 'TermEmployee02', 'Name', 'Salary', 'Some title', '2016-01-01', '2019-06-01', 4, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'TH8LF9', 'some-url/photo_default.jpg');
INSERT INTO Employee (isContractor, CompanyCode, OfficeCode, GroupCode, LastName, FirstName, EmploymentType, Title, HireDate, TerminationDate,SupervisorEmployeeNumber, YearsPriorExperience, Email, WorkPhone, WorkCell, LocationId, PhotoUrl)VALUES (0, '03', '03', '04', 'TermEmployee03', 'Name', 'Salary', 'Some title', '2017-01-01', '2020-06-01', 8, 0, 'something@acme.ca', '604-555-5555', '604-555-5555', 'LDFS8F3DDS', 'some-url/photo_default.jpg');

INSERT INTO EmployeeSkills (EmployeeNumber, SkillId) VALUES (11, '1');
INSERT INTO EmployeeSkills (EmployeeNumber, SkillId) VALUES (11, '2');
INSERT INTO EmployeeSkills (EmployeeNumber, SkillId) VALUES (11, '3');
INSERT INTO EmployeeSkills (EmployeeNumber, SkillId) VALUES (15, '1');
INSERT INTO EmployeeSkills (EmployeeNumber, SkillId) VALUES (15, '2');
INSERT INTO EmployeeSkills (EmployeeNumber, SkillId) VALUES (15, '3');
INSERT INTO EmployeeSkills (EmployeeNumber, SkillId) VALUES (15, '4');
INSERT INTO EmployeeSkills (EmployeeNumber, SkillId) VALUES (4, '6');
INSERT INTO EmployeeSkills (EmployeeNumber, SkillId) VALUES (4, '7');
INSERT INTO EmployeeSkills (EmployeeNumber, SkillId) VALUES (4, '8');
INSERT INTO EmployeeSkills (EmployeeNumber, SkillId) VALUES (8, '14');
INSERT INTO EmployeeSkills (EmployeeNumber, SkillId) VALUES (8, '15');
INSERT INTO EmployeeSkills (EmployeeNumber, SkillId) VALUES (8, '16');

/* insert admin */
INSERT INTO Admin (Username, Password) VALUES ('admin', '12345');

