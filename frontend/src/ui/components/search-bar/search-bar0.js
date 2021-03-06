import { React, useState, useEffect } from "react";
import { makeStyles } from "@material-ui/core/styles";
import {
  IconButton,
  TextField,
  Card,
  CardContent,
  Typography,
  InputAdornment,
} from "@material-ui/core";
import SearchIcon from "@material-ui/icons/Search";
import Autocomplete, {
  createFilterOptions,
} from "@material-ui/lab/Autocomplete";
import FilterIcon from "../../../assets/filter-icon.svg";
import { useHistory, useLocation } from "react-router-dom";
import * as qs from "query-string";
import storage from "../../../services/storage";
import "./search-bar.css";

const createUniqueOptions = createFilterOptions();

const useStyles = makeStyles((theme) => ({
  root: {
    padding: "2px 4px",
    display: "flex",
    alignItems: "center",
  },
  autocomplete: {
    width: 500,
    display: "flex",
  },
  text_field: {
    width: "100%",
    backgroundColor: "#EEF3F8",
  },
  divider: {
    height: 28,
    margin: 4,
  },
  card: {
    display: "flex",
    width: "100%",
  },
}));

const SearchBar = (props) => {
  const location = useLocation();
  let history = useHistory();
  const classes = useStyles();
  const [options, setOptions] = useState([]);
  const [value, setValue] = useState(null);
  const [inputValue, setInputValue] = useState("");
  const [selectedFilters, setSelectedFilters] = useState([]);

  const handleOnChange = (event, newValue) => {
    setValue(newValue);
    // console.log(selectedFilters);
    // console.log(newValue);
    setSelectedFilters([newValue]);

  };

  const handleInputChange = (event, newInputValue) => {
    setInputValue(newInputValue);
  };

  const handleGetOptionLabel = (option) => {
    // Value selected with enter, right from the input
    if (typeof option === "string") {
      return option;
    }
    // Add 'xxx' option created dynamically
    if (option.inputValue) {
      return option.inputValue;
    }
    // Regular option
    return option;
  };

  const handleInitiateSearch = async () => {
    // console.log("search button was clicked");
    let queries = await makeQueries();

    const stringified = qs.stringify(queries);

    history.push(`/search?${stringified}`);
  };

  const makeQueries = async () => {
    let queries;

    for (let i = 0; i < selectedFilters.length; i++) {
      queries = {
        ...queries,
        [selectedFilters[i].queryId]: selectedFilters[i].inputValue,
      };
    }

    //ONLY FOR DEMO, REMOVE LATER
    if (selectedFilters.length > 0) {
      storage.ss.setPair('search_key', JSON.stringify(queries));
    } else {
      let resp = storage.ss.getPair('search_key');
      if (resp != undefined || resp != null) {
        queries = JSON.parse(resp);
      }
    }
    return queries;
  }

  const handleOpenFilterModal = () => {
    // console.log("filter button was clicked");
  };

  const handleCreateNewOptions = (options, params) => {
    const filtered = createUniqueOptions(options, params);

    if (params.inputValue !== "") {
      if (params.inputValue.includes("@")) {
        filtered.push({
          inputValue: params.inputValue,
          filter_name: "Email",
          queryId: "email",
        });
      } else if (/^[0-9-()\-]+$/i.test(params.inputValue)) {
        filtered.push(
          {
            inputValue: params.inputValue,
            filter_name: "Work Cell",
            queryId: "workCell",
          },
          {
            inputValue: params.inputValue,
            filter_name: "Work Phone",
            queryId: "workPhone",
          }
        );
      } else {
        filtered.push({
          inputValue: params.inputValue,
          filter_name: "Name",
          queryId: "name",
        });
        filtered.push({
          inputValue: params.inputValue,
          filter_name: "Email",
          queryId: "email",
        });
      }
    }

    // console.log(filtered);

    return filtered;
  };

  return (
    <>
      <Autocomplete
        value={value}
        inputValue={inputValue}
        disableClearable
        freeSolo
        selectOnFocus
        clearOnBlur
        handleHomeEndKeys
        filterSelectedOptions
        className={classes.autocomplete}
        id="async-search"
        renderOption={(option) => (
          <>
            <Card className={classes.card}>
              <CardContent>
                <Typography variant="body1">{option.inputValue}</Typography>
                <Typography variant="caption">{option.filter_name}</Typography>
              </CardContent>
            </Card>
          </>
        )}
        options={[]}
        onInputChange={(event, newValue) => {
          handleInputChange(event, newValue);
        }}
        onChange={(event, newValue) => {
          handleOnChange(event, newValue);
        }}
        filterOptions={(options, params) => {
          return handleCreateNewOptions(options, params);
        }}
        getOptionLabel={(option) => {
          return handleGetOptionLabel(option);
        }}
        renderInput={(params) => (
          <>
            <TextField
              {...params}
              className="searchBox"
              variant="outlined"
              InputProps={{
                ...params.InputProps,
                endAdornment: (
                  <>
                    <InputAdornment position="end">
                      {location.pathname.includes("search") ? null : (
                        <IconButton
                          type="button"
                          id = "filter_open_button"
                          className={classes.iconButton}
                          onClick={handleOpenFilterModal}
                        >
                          <img width="24" height="24" src={FilterIcon}></img>
                        </IconButton>
                      )}
                      <IconButton
                        type="button"
                        className={classes.iconButton}
                        id="search_button_target"
                        onClick={handleInitiateSearch}
                      >
                        <SearchIcon color="primary" />
                      </IconButton>
                    </InputAdornment>
                  </>
                ),
              }}
            />
          </>
        )}
      />
    </>
  );
};

export default SearchBar;
