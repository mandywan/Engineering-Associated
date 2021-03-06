import React, { useEffect } from 'react';
import './pinned-profiles.css';
import storage from '../../../../services/storage';

import ProfileCard from '../../../../ui/components/profile-card';

const PinnedProfiles = (profiles) => {

  profiles = profiles.data;

  return (
    <div className="pinned-profiles">
      <div className="pinned-profiles-title">
        Pinned Profiles
      </div>
      <div className='pinned-profiles-wrapper'>
        <div className='pinned-profiles-container'>
          {profiles !== undefined ? 
          (
          ((profiles.length > 0) ? (
            profiles.map(profile => 
              (
                <ProfileCard key={profiles.indexOf(profile)} data={profile} tab="override" />
              )
              )
          ):(
           <div><center><p className="generic-msg msg-white">Frequently search for someone? Pin their profile here for quick access ⚡</p></center></div>
          ))
          ) : 
          (
            null
          )
          }
        </div>
      </div>
    </div>
  )

}

export default PinnedProfiles;
