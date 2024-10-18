import React, { useState } from 'react';

const HouseForm = () => {
    const [formData, setFormData] = useState({
        name: '',
        houseAddress: '',
        bio: '',
        numberOfPeople: 1,
        rentPricePerDay: 0,
        thumbnail: null,
        additionalImages: []
    });

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleFileChange = (e) => {
        const { name, files } = e.target;
        setFormData({ ...formData, [name]: files });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const formDataToSend = new FormData();

        for (const key in formData) {
            if (key === "additionalImages") {
                for (let i = 0; i < formData[key].length; i++) {
                    formDataToSend.append(key, formData[key][i]);
                }
            } else {
                formDataToSend.append(key, formData[key]);
            }
        }

        fetch('/Hosts/HostDetails', {
            method: 'POST',
            body: formDataToSend
        })
            .then(response => response.json())
            .then(data => console.log(data))
            .catch(error => console.error('Error:', error));
    };

    return (
        <form onSubmit={handleSubmit} encType="multipart/form-data">
            <div className="form-group">
                <label htmlFor="name">Name</label>
                <input type="text" name="name" className="form-control" value={formData.name} onChange={handleInputChange} required />
            </div>

            <div className="form-group">
                <label htmlFor="houseAddress">House Address</label>
                <input type="text" name="houseAddress" className="form-control" value={formData.houseAddress} onChange={handleInputChange} required />
            </div>

            <div className="form-group">
                <label htmlFor="thumbnail">Thumbnail Picture</label>
                <input type="file" name="thumbnail" className="form-control" onChange={handleFileChange} required />
            </div>

            <div className="form-group">
                <label htmlFor="additionalImages">Additional Pictures (8 Max)</label>
                <input type="file" name="additionalImages" multiple className="form-control" onChange={handleFileChange} required />
            </div>

            <div className="form-group">
                <label htmlFor="bio">Bio</label>
                <textarea name="bio" className="form-control" value={formData.bio} onChange={handleInputChange} required></textarea>
            </div>

            <div className="form-group">
                <label htmlFor="numberOfPeople">Number of People</label>
                <input type="number" name="numberOfPeople" className="form-control" value={formData.numberOfPeople} onChange={handleInputChange} required />
            </div>

            <div className="form-group">
                <label htmlFor="rentPricePerDay">Rent Price Per Day</label>
                <input type="number" name="rentPricePerDay" className="form-control" value={formData.rentPricePerDay} onChange={handleInputChange} required />
            </div>

            <button type="submit" className="btn btn-primary">Submit</button>
        </form>
    );
};

export default HouseForm;