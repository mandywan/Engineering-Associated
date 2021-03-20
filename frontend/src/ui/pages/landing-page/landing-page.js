import React, { useEffect } from 'react';
import Header from './landing-header';
import PinnedProfiles from './pinned-profiles';
import ResumeSearch from './resume-search';
import ExploreAELocations from './explore-ae-locations';
import Filter_modal from '../../components/filter_modal/filter_modal';
import '../../components/filter_modal/filter_modal.css'
import filters from '../../../services/filters';
import storage from '../../../services/storage';

const LandingPage = () => {


  useEffect(async()=> {
    filters.clear();
    storage.ss.setPair('search_key', null);
    storage.ss.setPair('current_search', null);
    if (!storage.ls.getPair('searchHistory')) {
      storage.ls.setPair('searchHistory', JSON.stringify([]));
    }
    console.log('all clear');
  }, [] );
  return (
    <div>
      <Header />
      <PinnedProfiles />
      <ExploreAELocations />
      <Filter_modal/>
    </div>
  );

}

export default LandingPage;